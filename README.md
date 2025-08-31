# TraceDatababeOld4

WinForms tool untuk **trace `cq_action`** (dan append `cq_task`) dari **MySQL 4.0.x**.  
Target: **.NET Framework 3.5** + **MySql.Data 1.0.x** + **ILMerge** (single EXE).

---

## Features
- Connect / Disconnect MySQL 4.0
- Pilih database → trace **NPC Action ID**
  - follow `id_next` + semua `id_next*`
  - queue `id_nextfail`
  - type-based follow: `0102, 0122, 0134, 0188, 1412, 2012, 2020, 8000–8003, 8005, 8006`
- Output:
  - `REPLACE INTO cq_action ...` ikut **susunan kolum sebenar**
  - append `REPLACE INTO cq_task ...` ikut turutan lawatan
- Butang **Select All**, **Copy All**, **Clear**
- Build **single EXE** automatik (ILMerge)

---

## Requirements
- Windows x64
- Visual Studio 2019/2022 (workload **.NET desktop development**)
- **.NET Framework 3.5** Targeting Pack
- **MySQL Server 4.0.x** dengan jadual `cq_action` & `cq_task`
- **NuGet**: `ILMerge` **3.0.41**
- **Connector lama**: `MySql.Data.dll` **v1.0.10.x** (rujuk *Setup*)

> MySQL 4.0 guna protokol lama → **parameter marker `?name`** (bukan `@name`).

---

## Setup
1. **Clone**
   ```bash
   git clone https://github.com/<user>/TraceDatababeOld4.git
   ```
2. **Buka solution** dalam VS.
3. **Restore NuGet** (ILMerge):
   ```
   Tools → NuGet Package Manager → Package Manager Console
   Update-Package -Reinstall
   ```
4. **MySql.Data.dll (manual)**  
   - Fail DLL dirujuk oleh `.csproj` melalui `HintPath`. Update ke lokasi sebenar jika perlu:
     ```xml
     <Reference Include="MySql.Data, Version=1.0.10.0, PublicKeyToken=c5687fc88969c44d">
       <SpecificVersion>true</SpecificVersion>
       <HintPath>..\..\..\..\Desktop\OLD 4 mysql.data\MySql.Data.dll</HintPath>
       <Private>true</Private>
     </Reference>
     ```
5. **Set configuration**: **Release | x64**

---

## Build
- **Build → Rebuild Solution**
- Output:
  - `bin\Release\TraceDatababeOld4.exe` (biasa)
  - `bin\Release\TraceDatababeOld4-merged.exe` (**single EXE** via ILMerge)
  - (Jika target `AfterBuild` diaktifkan) → salinan ke `dist\TraceDatababeOld4.exe`

> Projek dah sediakan `AfterBuild` ILMerge. Jika perlu tulis manual, contoh:
```xml
<Target Name="AfterBuild">
  <Exec Command="&quot;$(ILMergeConsolePath)&quot; /target:winexe /ndebug /log /targetplatform:v2,&quot;%WINDIR%\Microsoft.NET\Framework64\v2.0.50727&quot; /out:&quot;$(TargetDir)TraceDatababeOld4-merged.exe&quot; &quot;$(TargetPath)&quot; &quot;$(TargetDir)MySql.Data.dll&quot;" />
  <MakeDir Directories="$(SolutionDir)dist" />
  <Copy SourceFiles="$(TargetDir)TraceDatababeOld4-merged.exe"
        DestinationFiles="$(SolutionDir)dist\TraceDatababeOld4.exe" />
</Target>
```
*Mesin 32-bit? Tukar `Framework64` → `Framework`.*

---

## Run
- Guna **`TraceDatababeOld4-merged.exe`** (portable, tak perlu installer)
- Pastikan **.NET Framework 3.5** aktif di PC user

---

## Cara Guna
1. **Connect**: isi Host/Port/User/Password → **Connect**
2. **Select DB**: pilih DB → **Select**
3. **Trace**: isi **NPC Action ID** → **Trace →**  
   Output SQL akan muncul. Guna **Select All / Copy All / Clear**.

---

## Struktur
```
TraceDatababeOld4/
 ├─ MainForm.cs                // logic & DB helpers (param marker '?')
 ├─ MainForm.Designer.cs       // UI
 ├─ Program.cs                 // entry point
 ├─ Properties/…
 ├─ track.ico
 ├─ packages.config            // ILMerge 3.0.41
 └─ TraceDatababeOld4.csproj   // .NET 3.5, x64, ILMerge AfterBuild
```

---

## Troubleshooting
- **ILMerge exited code 3**  
  → Path ILMerge salah. Pastikan `$(ILMergeConsolePath)` wujud (dari `..\packages\ILMerge.3.0.41\build\ILMerge.props`).  
  Semak log `ILMerge.log` di output dir.
- **Trace kosong**  
  - Pastikan `cq_action` ada data & ID wujud:
    ```sql
    SELECT COUNT(*) FROM cq_action;
    SELECT * FROM cq_action ORDER BY id LIMIT 5;
    ```
  - Pastikan gunakan `?param` (projek ini sudah guna).
- **Tak boleh connect**  
  - Semak kredensial & version server 4.0.x (tanpa SSL).
  - User mesti ada hak ke DB terpilih.

---
