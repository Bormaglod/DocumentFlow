; -- 64Bit.iss --
; Demonstrates installation of a program built for the x64 (a.k.a. AMD64)
; architecture.
; To successfully run this installation and the program it installs,
; you must have a "x64" edition of Windows.

; SEE THE DOCUMENTATION FOR DETAILS ON CREATING .ISS SCRIPT FILES!

; ��� ����������
#define   Name       "DocumentFlow"
; ������ ����������
#define   Version    "2023.10.5"
; ��� ������������ ������
#define   ExeName    "DocumentFlow.exe"

[Setup]

; ������ ����������, ������������ ��� ���������
AppId={{78217AAE-9D17-403C-9087-17F8FDE0184A}}
AppName={#Name}
AppVersion={#Version}
WizardStyle=modern

; ���� ��������� ��-���������
DefaultDirName={autopf}\{#Name}

; ��� ������ � ���� "����"
DefaultGroupName={#Name}
UninstallDisplayIcon={app}\{#Name}.exe

; ��������� ������
Compression=lzma2
SolidCompression=yes

; �������, ���� ����� ������� ��������� setup � ��� ������������ �����
OutputDir=C:\Projects\DocumentFlow\DocumentFlow.Install
OutputBaseFileName={#Name}-{#SetupSetting("AppVersion")}-x64-installer

; ���� ������
SetupIconFile=C:\Projects\DocumentFlow\DocumentFlow.Install\setup.ico

; "ArchitecturesAllowed=x64" specifies that Setup cannot run on
; anything but x64.
ArchitecturesAllowed=x64
; "ArchitecturesInstallIn64BitMode=x64" requests that the install be
; done in "64-bit mode" on x64, meaning it should use the native
; 64-bit Program Files directory and the 64-bit view of the registry.
ArchitecturesInstallIn64BitMode=x64

[Files]
Source: "C:\Projects\DocumentFlow\DocumentFlow\bin\x64\Release\DocumentFlow.exe"; DestDir: "{app}"
Source: "C:\Projects\DocumentFlow\DocumentFlow\bin\x64\Release\*"; DestDir: "{app}"; Flags: recursesubdirs createallsubdirs

[Icons]
Name: "{group}\DocumentFlow"; Filename: "{app}\{#ExeName}"

[Run]
Filename: "{app}\{#ExeName}"; Description: "Launch application"; Flags: postinstall nowait skipifsilent unchecked