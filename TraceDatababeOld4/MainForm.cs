// Works with MySql.Data 1.0.x + MySQL 4.0.x  (Target .NET Framework 3.5)
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TraceDatababeOld4
{
    public partial class MainForm : Form
    {

        private static readonly Encoding Gbk = Encoding.GetEncoding(936); // GBK/CP936

        private MySqlConnection _conn;
        private string _host, _user, _pass, _selectedDb;

        public MainForm()
        {
            InitializeComponent();
            btnConnect.Click += BtnConnect;
            btnSelectDb.Click += BtnSelDb;
            btnTrace.Click += BtnTrace;

            // NEW: output helpers
            btnSelectAll.Click += (s, e) => { rtbOutput.SelectAll(); rtbOutput.Focus(); };
            btnCopyAll.Click += (s, e) => { rtbOutput.SelectAll(); rtbOutput.Copy(); };
            btnClear.Click += (s, e) => { rtbOutput.Clear(); rtbOutput.Focus(); };

            // optional (pastikan shortcut Ctrl+A/C berfungsi)
            rtbOutput.ShortcutsEnabled = true;
        }

        // ---- utils ------------------------------------------------------------

        // .NET 3.5: no string.IsNullOrWhiteSpace
        private static bool IsNullOrWhite(string s) { return s == null || s.Trim().Length == 0; }

        // Connector/NET 1.0.x: no AddWithValue
        // ganti helper param lama
        private static void AddParam(MySqlCommand cmd, string name, object value)
        {
            // name mesti bermula dengan '?'
            if (!name.StartsWith("?")) name = "?" + name.TrimStart('@');
            var p = new MySqlParameter(name, value ?? DBNull.Value);
            cmd.Parameters.Add(p);
        }


        private string ConnStr(string host, string user, string pass, string db)
        {
            uint port = 3306;
            uint.TryParse(txtPort.Text.Trim(), out port);

            var sb = new StringBuilder();
            sb.AppendFormat("Server={0};Port={1};User Id={2};Password={3};CharSet=gbk;", host, port, user, pass);
            if (!IsNullOrWhite(db)) sb.AppendFormat("Database={0};", db);
            return sb.ToString(); // simple utk MySQL 4.0 + Connector lama
        }


        // ---- UI handlers ------------------------------------------------------

        private void BtnConnect(object s, EventArgs e)
        {
            // disconnect toggle
            if (_conn != null && _conn.State == ConnectionState.Open)
            {
                try { _conn.Close(); } catch { }
                _conn.Dispose(); _conn = null;
                _selectedDb = null;
                cbDatabases.DataSource = null;
                cbDatabases.Enabled = false;
                btnSelectDb.Enabled = false;
                txtNpcActionId.Enabled = false;
                btnTrace.Enabled = false;
                if (lblConnStatus != null) lblConnStatus.Text = "Status: Not connected";
                btnConnect.Text = "Connect";
                return;
            }

            _host = txtHost.Text.Trim();
            _user = txtUser.Text.Trim();
            _pass = txtPass.Text;

            if (IsNullOrWhite(_host) || IsNullOrWhite(_user))
            {
                MessageBox.Show("Host/User required.");
                return;
            }

            try
            {
                _conn = new MySqlConnection(ConnStr(_host, _user, _pass, null));
                _conn.Open();
                InitSessionEncoding(); // <<< tambah baris ni

                using (var cmd = (MySqlCommand)_conn.CreateCommand())
                {
                    cmd.CommandText = "SHOW DATABASES";
                    using (var rd = cmd.ExecuteReader())
                    {
                        var list = new List<string>();
                        while (rd.Read())
                        {
                            var db = rd.GetString(0);
                            if (!new[] { "mysql", "information_schema", "performance_schema", "sys" }
                                .Contains(db, StringComparer.OrdinalIgnoreCase))
                                list.Add(db);
                        }
                        cbDatabases.DataSource = list;
                    }
                }

                cbDatabases.Enabled = true;
                btnSelectDb.Enabled = true;
                btnConnect.Text = "Disconnect";
                if (lblConnStatus != null)
                {
                    var portText = txtPort.Text.Trim();
                    lblConnStatus.Text = "Status: Connected to server " + _host + ":" + (IsNullOrWhite(portText) ? "3306" : portText) + " (no DB selected)";
                }
            }
            catch (Exception ex)
            {
                if (_conn != null) { try { _conn.Dispose(); } catch { } }
                _conn = null;
                MessageBox.Show("Connect failed: " + ex.Message);
            }
        }

        private void BtnSelDb(object s, EventArgs e)
        {
            var db = cbDatabases.SelectedItem as string;
            if (IsNullOrWhite(db)) { MessageBox.Show("Please select a database."); return; }

            try
            {
                if (_conn != null) { try { _conn.Close(); } catch { } _conn.Dispose(); }
                _conn = new MySqlConnection(ConnStr(_host, _user, _pass, db));
                _conn.Open();
                InitSessionEncoding(); // <<< tambah baris ni

                _selectedDb = db;
                txtNpcActionId.Enabled = true;
                btnTrace.Enabled = true;

                if (lblConnStatus != null)
                {
                    var portText = txtPort.Text.Trim();
                    lblConnStatus.Text = "Status: Connected to " + _selectedDb + " on " + _host + ":" + (IsNullOrWhite(portText) ? "3306" : portText);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Select DB failed: " + ex.Message);
            }
        }

        // BtnTrace – tambah check awal kalau root tak wujud
        private void BtnTrace(object s, EventArgs e)
        {
            if (_conn == null || _conn.State != ConnectionState.Open || IsNullOrWhite(_selectedDb))
            { MessageBox.Show("Connect & select DB first."); return; }

            int rootId;
            if (!int.TryParse(txtNpcActionId.Text.Trim(), out rootId) || rootId <= 0)
            { MessageBox.Show("Invalid ID."); return; }

            if (!RowExists(rootId))
            {
                MessageBox.Show("cq_action.id = " + rootId + " tiada dalam DB terpilih.");
                return;
            }

            try
            {
                var sql = TraceActionsPhpStyle(rootId);
                rtbOutput.Text = sql.Length == 0 ? "-- No data found" : sql;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Trace failed: " + ex.Message);
            }
        }


        // ---- DB helpers -------------------------------------------------------

        private List<string> GetColumns(string table)
        {
            using (var cmd = (MySqlCommand)_conn.CreateCommand())
            {
                cmd.CommandText = "SHOW COLUMNS FROM `" + table + "`";
                var list = new List<string>();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read()) list.Add(rd.GetString(0)); // index 0 = column name
                }
                return list;
            }
        }

        // GetRowById – TUKAR @id -> ?id
        private Dictionary<string, object> GetRowById(string table, string key, long id)
        {
            using (var cmd = (MySqlCommand)_conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM `" + table + "` WHERE `" + key + "`=?id LIMIT 1";
                AddParam(cmd, "?id", id);
                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return null;
                    var d = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                    for (int i = 0; i < rd.FieldCount; i++)
                        d[rd.GetName(i)] = rd.IsDBNull(i) ? null : rd.GetValue(i);
                    return d;
                }
            }
        }


        // RowExists – TUKAR @i -> ?i
        private bool RowExists(long x)
        {
            using (var cmd = (MySqlCommand)_conn.CreateCommand())
            {
                cmd.CommandText = "SELECT 1 FROM `cq_action` WHERE `id`=?i LIMIT 1";
                AddParam(cmd, "?i", x);
                var o = cmd.ExecuteScalar();
                return o != null;
            }
        }


        // ---- SQL builders -----------------------------------------------------

        // BuildReplaceSql/BuildInsertSqlNoColumns – pastikan ToArray()
        private string BuildReplaceSql(string table, List<string> cols, Dictionary<string, object> row)
        {
            var vals = cols.Select(c => SqlLit(row.ContainsKey(c) ? row[c] : null)).ToArray();
            return "REPLACE INTO `" + table + "` VALUES (" + string.Join(", ", vals) + ");";
        }
        private string BuildInsertSqlNoColumns(string table, List<string> cols, Dictionary<string, object> row)
        {
            var vals = cols.Select(c => SqlLit(row.ContainsKey(c) ? row[c] : null)).ToArray();
            return "REPLACE INTO `" + table + "` VALUES (" + string.Join(", ", vals) + ");";
        }


        private string SqlLit(object v)
        {
            if (v == null || v == DBNull.Value) return "NULL";
            switch (Type.GetTypeCode(v.GetType()))
            {
                case TypeCode.Boolean: return ((bool)v) ? "1" : "0";
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return Convert.ToString(v, System.Globalization.CultureInfo.InvariantCulture);
                case TypeCode.DateTime:
                    return "'" + ((DateTime)v).ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) + "'";
                default:
                    string s = v.ToString() ?? "";
                    s = s.Replace("\\", "\\\\").Replace("'", "\\'");
                    return "'" + s + "'";
            }
        }


        // ---- misc -------------------------------------------------------------

        private string GetTypeAsFourDigits(Dictionary<string, object> row, string key)
        {
            object v;
            if (!row.TryGetValue(key, out v) || v == null) return "";
            string str = (Convert.ToString(v) ?? "").Trim();
            int n;
            if (int.TryParse(str, out n)) return n.ToString("D4");
            if (str.Length == 4 && str.All(char.IsDigit)) return str;
            string d = new string(str.Where(char.IsDigit).ToArray());
            return (d.Length >= 4) ? d.Substring(d.Length - 4) : d;
        }

        private IEnumerable<string> SplitWs(string s)
        {
            return (s ?? "").Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
        }

        // ---- trace core -------------------------------------------------------

        private string TraceActionsPhpStyle(int startId)
        {
            var cols = GetColumns("cq_action");
            if (cols.Count == 0) throw new Exception("Table cq_action not found.");

            bool hasIdNext = cols.Any(c => c.Equals("id_next", StringComparison.OrdinalIgnoreCase));
            bool hasIdNextFail = cols.Any(c => c.Equals("id_nextfail", StringComparison.OrdinalIgnoreCase));
            var moreNextCols = cols.Where(c => c.StartsWith("id_next", StringComparison.OrdinalIgnoreCase)
                                            && !c.Equals("id_next", StringComparison.OrdinalIgnoreCase)
                                            && !c.Equals("id_nextfail", StringComparison.OrdinalIgnoreCase)).ToList();

            var traceTypes = new HashSet<string>(StringComparer.Ordinal)
            { "0102","2012","0122","0188","1412","0134","2020","8000","8001","8002","8003","8005","8006" };

            var visited = new HashSet<long>();
            var visitOrder = new List<long>();
            var queued = new HashSet<long>();
            var mainQ = new Queue<long>();
            var auxQ = new Queue<long>();

            // enqueue helpers (no local functions in C# 3)
            Action<long> EnqMain = delegate (long x) { if (x > 0 && !visited.Contains(x) && queued.Add(x)) mainQ.Enqueue(x); };
            Action<long> EnqAux = delegate (long x) { if (x > 0 && !visited.Contains(x) && queued.Add(x)) auxQ.Enqueue(x); };

            EnqMain(startId);

            var sb = new StringBuilder();
            sb.AppendLine("-- Generated by TraceDatabaseOld");
            sb.AppendLine("-- Root ID: " + startId);

            bool lastMain = true;

            while (mainQ.Count > 0 || auxQ.Count > 0)
            {
                bool fromMain = mainQ.Count > 0;
                long id = fromMain ? mainQ.Dequeue() : auxQ.Dequeue();

                if (!fromMain && lastMain) sb.AppendLine();
                lastMain = fromMain;

                if (!visited.Add(id)) continue;
                visitOrder.Add(id);

                var row = GetRowById("cq_action", "id", id);
                if (row == null) continue;

                sb.AppendLine(BuildReplaceSql("cq_action", cols, row));

                // MAIN links
                if (hasIdNext)
                {
                    object vNext;
                    if (row.TryGetValue("id_next", out vNext) && vNext != null)
                    {
                        long idNext;
                        if (long.TryParse(Convert.ToString(vNext), out idNext) && idNext > 0 && RowExists(idNext))
                            EnqMain(idNext);
                    }
                }

                foreach (string c in moreNextCols)
                {
                    object v;
                    if (!row.TryGetValue(c, out v) || v == null) continue;
                    long n;
                    if (long.TryParse(Convert.ToString(v), out n) && n > 0 && RowExists(n))
                        EnqMain(n);
                }

                // AUX: id_nextfail
                if (hasIdNextFail)
                {
                    object vFail;
                    if (row.TryGetValue("id_nextfail", out vFail) && vFail != null)
                    {
                        long idFail;
                        if (long.TryParse(Convert.ToString(vFail), out idFail) && idFail > 0 && RowExists(idFail))
                            EnqAux(idFail);
                    }
                }

                // AUX: type-based
                string typeStr = GetTypeAsFourDigits(row, "type");
                string param = (row.ContainsKey("param") && row["param"] != null) ? (Convert.ToString(row["param"]) ?? "") : "";

                if (traceTypes.Contains(typeStr))
                {
                    if (typeStr == "0122")
                    {
                        foreach (var tok in SplitWs(param))
                        {
                            long n;
                            if (long.TryParse(tok, out n) && n > 0 && RowExists(n)) EnqAux(n);
                        }
                    }
                    else if (new[] { "1412", "8000", "8001", "8002", "8003", "8005", "8006" }.Contains(typeStr))
                    {
                        string tok = param.Trim();
                        long n;
                        if (long.TryParse(tok, out n) && n > 0 && RowExists(n)) EnqAux(n);
                    }
                    else if (typeStr == "2020")
                    {
                        var parts = SplitWs(param).ToList();
                        long n;
                        if (parts.Count >= 6 && long.TryParse(parts[5], out n) && n > 0 && RowExists(n))
                            EnqAux(n);
                    }
                    else if (typeStr == "2012")
                    {
                        var parts = SplitWs(param).ToList();
                        long n;
                        if (parts.Count >= 2 && long.TryParse(parts[1], out n) && n > 0 && RowExists(n))
                            EnqAux(n);
                    }
                    else if (typeStr == "0188")
                    {
                        foreach (var entry in (param ?? "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            var parts = SplitWs(entry).ToList();
                            long n;
                            if (parts.Count >= 3 && long.TryParse(parts[2], out n) && n > 0 && RowExists(n))
                                EnqAux(n);
                        }
                    }
                    else if (typeStr == "0134")
                    {
                        var parts = SplitWs(param).ToList();
                        long n;
                        if (parts.Count >= 1 && long.TryParse(parts[0], out n) && n > 0 && RowExists(n))
                            EnqAux(n);
                    }
                    else
                    {
                        var m = Regex.Match(param ?? "", @"(\d{6,})$");
                        long n;
                        if (m.Success && long.TryParse(m.Groups[1].Value, out n) && n > 0 && RowExists(n))
                            EnqAux(n);
                    }
                }
            }

            // append cq_task rows
            var taskCols = GetColumns("cq_task");
            if (taskCols.Count > 0)
            {
                bool first = true;
                foreach (long id in visitOrder)
                {
                    var trow = GetRowById("cq_task", "id", id);
                    if (trow == null) continue;

                    if (first) { sb.AppendLine(); sb.AppendLine("-- Related cq_task rows"); first = false; }
                    sb.AppendLine(BuildInsertSqlNoColumns("cq_task", taskCols, trow));
                }
            }

            return sb.ToString();
        }

        private void InitSessionEncoding()
        {
            try
            {
                using (var cmd = (MySqlCommand)_conn.CreateCommand())
                {
                    cmd.CommandText = "SET NAMES gbk";
                    cmd.ExecuteNonQuery();

                    // opsyen tambahan (sesetengah build lama suka explicit)
                    try { cmd.CommandText = "SET character_set_results=gbk"; cmd.ExecuteNonQuery(); } catch { }
                }
            }
            catch { /* jika build 4.0 tertentu tak support, biar CharSet handle */ }
        }

        private void BtnSaveGbk(object s, EventArgs e)
        {
            using (var dlg = new SaveFileDialog { Filter = "SQL File|*.sql", FileName = "trace.sql" })
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    System.IO.File.WriteAllText(dlg.FileName, rtbOutput.Text, Gbk);
                    MessageBox.Show("Saved as GBK (936).");
                }
            }
        }

    }
}
