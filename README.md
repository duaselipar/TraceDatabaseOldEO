# TraceDatabaseOldEO

**TraceDatabaseOldEO** is a Windows Forms tool for tracing `cq_action` (and appending related `cq_task`) records from an **Eudemons Online private server** running **MySQL 4.0.x**.  
It helps server developers and administrators to follow NPC Action IDs, next chains, and generate SQL outputs quickly.

---

## ✨ Features
- 🔌 Connect / Disconnect MySQL 4.0 with one click.
- 📂 Select database, trace NPC Action ID with `id_next`, `id_next*`, and `id_nextfail`.
- 🔄 Type-based follow support: `0102, 0122, 0134, 0188, 1412, 2012, 2020, 8000–8003, 8005, 8006`.
- 📝 Output SQL in correct column order:
  - `REPLACE INTO cq_action ...`
  - `REPLACE INTO cq_task ...`
- 📋 Quick actions: **Select All**, **Copy All**, **Clear**.
- 📦 Single portable EXE build using ILMerge.

---

## 🛠 Requirements
- Windows x64
- .NET Framework 3.5
- Visual Studio 2019/2022 (.NET desktop workload)
- MySQL Server 4.0.x with `cq_action` & `cq_task`
- Old connector: **MySql.Data.dll v1.0.10.x**
- NuGet: ILMerge 3.0.41

---

## 🚀 How to Use
1. Launch `TraceDatababeOld4.exe` (merged single EXE).
2. Enter MySQL connection details (host, port, username, password).
3. Click **Connect** to connect to MySQL 4.0.
4. Select the target database.
5. Enter the **NPC Action ID** to trace.
6. Click **Trace Action** → SQL output will appear.
7. Use **Copy All** to copy results.

---

## 📂 Build Notes (for developers)
- Build in **Release | x64**.
- ILMerge creates `TraceDatabaseOldEO.exe` in `dist/` folder.
- Ensure `MySql.Data.dll` path in `.csproj` points to the correct DLL.

---

## ⚠️ Troubleshooting
- **Empty trace** → check `cq_action` table has data.
- **Connection failed** → MySQL 4.0 only, no SSL, user must have DB access.
- **ILMerge error** → verify ILMerge path and version.

---

© 2025 DuaSelipar Dev Hub
