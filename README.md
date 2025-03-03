# GTKSystem.Windows.Forms

### 介绍
**Visual Studio原生开发，无需学习，一次编译，跨平台运行**.
C#桌面应用程序跨平台（windows、linux、macos）开发框架，基于GTK组件开发，使用该框架开发项目，Visual studio可以使用C#的原生winform表单窗体设计器，相同的属性、方法、事件，C#原生开发即可，无需学习。一次编译，跨平台运行。 便于开发跨平台winform软件，便于将C# winform软件升级为跨平台软件。

项目官网：[https://www.gtkapp.com](https://www.gtkapp.com)   

### 软件架构

使用DotNet Csharp为开发语言，使用GTK3.24.24.95作为表单UI，重写C#的System.Windows.Forms组件，在应用时，兼容原生C#程序组件。

### 安装教程
默认的情况下，visual studio从Nuget引用GtkSharp编译时，就会自动下载Gtk.zip运行时安装包，并自动解压安装。本开源项目下载包也包含Gtk.zip包，可手动安装。以下是三种环境安装方法：

1、安装GtkSharp后，编译你的工程项目，自动安装（此库不是最新的，有些功能可能有Bug）  
安装GtkSharp后，编译你的工程项目时，会自动下载gtk.zip解压到目录$(LOCALAPPDATA)\Gtk\3.24.24配置Gtk环境，目前国内网络限制，可能会出现无法下载的错误。
如果无法自动下载，本项目提供下载 [https://gitee.com/easywebfactory/GTK-for-Windows/tree/master/Dependencies](https://gitee.com/easywebfactory/GTK-for-Windows/tree/master/Dependencies)。
也可以下载https://github.com/GtkSharp/Dependencies，把文件解压后放到$(LOCALAPPDATA)\Gtk\3.24.24目录即可。
ps: $(LOCALAPPDATA)为电脑的AppData\Local文件夹,如：C:\Users\chj\AppData\Local\Gtk\3.24.24

2、下载exe安装包安装（建议使用此方法下载安装，获取最新库，本项目下载包已经包含此安装包） 
本项目提供下载 [https://gitee.com/easywebfactory/GTK-for-Windows/tree/master/Dependencies](https://gitee.com/easywebfactory/GTK-for-Windows/tree/master/Dependencies)。
方法1在国内可能会有网络障碍，并且是比较旧的运行时库，可能有Bug，建议用此方法获取最新版本安装：下载[https://github.com/tschoonj/GTK-for-Windows-Runtime-Environment-Installer](https://github.com/tschoonj/GTK-for-Windows-Runtime-Environment-Installer)，安装后配置电脑变量环境：
```
你可以打开电脑属性配置，或者执行以下.bat命令：
@set GTK3R_PREFIX=C:\Program Files\GTK3-Runtime Win64
@echo set PATH=%GTK3R_PREFIX%;%%PATH%%
@set PATH=%GTK3R_PREFIX%;%PATH%
```
3、使用MSYS软件平台安装，具体操作请网上查询（可以获取最新库）

windows安装DotNet环境：
```
  从微软官网下载安装包https://dotnet.microsoft.com/zh-cn/download
```

**桌面版linux操作系统通常已经预装GTK环境，不需要再安装GTK，只需安装DotNet SDK即可运行本框架。**

# Arch
sudo apt install gtk3

# Fedora
sudo apt install gtk3            # Binary package
sudo apt install gtk3-devel      # Development package

# From MSYS2:
pacman -S mingw-w64-ucrt-x86_64-gtk3

*检查环境情况（需要安装pkg-config）：
    pkg-config --cflags --libs gtk+-3.0
*查找gtk的安装包目录：
    ldconfig -p | grep gtk
```
linux安装DotNet环境：
```
  安装方法可以查看微软官网教程：https://learn.microsoft.com/zh-cn/dotnet/core/install/linux-scripted-manual
```

### 开发教程
1.  项目工程框架选择“window应用程序”改配置UseWindowsForms为false或“控制台应用程序”，.net6及以上版本
2.  NulGet安装GtkSharp(3.24.24.95)、GTKSystem.Windows.Forms、GTKSystem.Windows.FormsDesigner
3.  检查form表单是否有使用图像资源，如使用需新建System.Resources.ResourceManager和System.ComponentModel.ComponentResourceManager，具体请看下面内容。
4.  编译工程，执行本项目的开发插件菜单“修复窗体设计器”，或者手动在obj目录下创建.designer.runtimeconfig.json，请看下面第5点。

### 如何运行软件
1. windows下：直接编译发布运行，Debug目录的demo_app.exe文件或demo_app.dll文件都可以直接运行。
2. linux和macos上：执行命令运行dotnet demo_app.dll。
3. 使用本框架的工程项目也可以在linux系统上编译发布，可以生成linux系统专用文件（无后缀名的文件），此文件可以直接双击启动应用

### VisualStudio插件安装

## Install .NET Environment on Linux
To install .NET on Linux, refer to the official Microsoft guide:  
[Microsoft .NET Install Guide for Linux](https://learn.microsoft.com/dotnet/core/install/linux-scripted-manual)

---

## Visual Studio Plugin Installation

### Tool 1: NuGet Installation
Install the `GTKSystem.Windows.FormsDesigner` library from NuGet.  
This library helps fix form designers during project compilation.

### Tool 2: VSIX Plugin Installation
Download the plugin tool, close Visual Studio, and double-click the `GTKWinformVSIXProject.vsix` file to install it.  
*(Required if no Form template is available for projects in this framework.)*

The plugin installs two functionalities:
1. **Form Templates**: Adds templates for Forms and User Controls to "New Item" options.
2. **Right-Click Menu**: Adds a context menu to projects for specific GTKWinForms tasks.

![Visual Studio Plugin Screenshot](pic/vs_vsix.jpeg)

## Development Instructions

### Configuration Steps

1. **Create `System.Resources.ResourceManager` Class**
   - Create a `System.Resources.ResourceManager` class in your project.
   - Inherit from `GTKSystem.Resources.ResourceManager` to override the native `System.Resources.ResourceManager`.
   - This is used for reading project resource files and images.  
   *(If your project does not use resource files or images, this step is unnecessary.)*

2. **Create `System.ComponentModel.ComponentResourceManager` Class**
   - Create a `System.ComponentModel.ComponentResourceManager` class in your project.
   - Inherit from `GTKSystem.ComponentResourceManager` to override the native `System.ComponentModel.ComponentResourceManager`.
   - This class enables reading project resources and images (calls `GTKSystem.Resources.ResourceManager`).  
   *(If your project does not use resource files or images, this step is unnecessary.)*

3. **Modify `GTKWinFormsApp.csproj`**
   - Configure the project file by setting `UseWindowsForms` to `false`.  
   - Example configuration:
     ```xml
     <Project Sdk="Microsoft.NET.Sdk.WindowsDesktop">
       <PropertyGroup>
         <OutputType>WinExe</OutputType>
         <TargetFramework>net8.0</TargetFramework>
         <UseWindowsForms>false</UseWindowsForms>
     ```

4. **Add References**
   - Add the following references to your project:
     - `GTKSystem.Windows.Forms` (mandatory)
     - `System.Resources.Extensions` (optional, used only for specific design-time exceptions in Visual Studio).

5. **Configure Runtime Files**
   - Ensure the runtime configuration files include the following changes for Visual Studio form designers:  
     Example `GTKWinFormsApp.designer.runtimeconfig.json`:
     ```json
     {
       "runtimeOptions": {
         "tfm": "net8.0",
         "framework": {
           "name": "Microsoft.WindowsDesktop.App",
           "version": "8.0.0"
         }
       }
     }
     ```

---

## Support and Resources

**Enterprise Services**: [https://www.gtkapp.com/vipservice](https://www.gtkapp.com/vipservice)  

![Support GTKSystem](pic/love_reward_qrcode_.png)  
![Contact GTKSystem](pic/contact_weixin.png)

---

## Common Issues

**Why can't I open the Form Designer?**  
Follow these steps to resolve the issue:
1. Compile the project.
2. Open the Form Designer.
3. If it still doesn't open:
   - Close the Form Designer.
   - Recompile the project.
   - Restart Visual Studio.
   - Open the Form Designer again.

---

## Contribution
You can contribute to the development of this framework:

- [Gitee Repository](https://gitee.com/easywebfactory)  
- [GitHub Repository](https://github.com/easywebfactory)  
- [CSDN Blog](https://blog.csdn.net/auto_toyota)  

---

## Update History
For detailed updates, check: [Update History >>](UpdateHistory.md)

