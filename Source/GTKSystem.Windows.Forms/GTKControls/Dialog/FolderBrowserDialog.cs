﻿/*
 * 基于GTK组件开发，兼容原生C#控件winform界面的跨平台界面组件。
 * 使用本组件GTKSystem.Windows.Forms代替Microsoft.WindowsDesktop.App.WindowsForms，一次编译，跨平台windows、linux、macos运行
 * 技术支持438865652@qq.com，https://gitee.com/easywebfactory, https://github.com/easywebfactory, https://www.cnblogs.com/easywebfactory
 * author:chenhongjin
 */

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace System.Windows.Forms
{
    public sealed class FolderBrowserDialog : FileDialog
    {
        public FolderBrowserDialog()
        {
            
        }
        public override void Reset()
        {
            RootFolder = Environment.SpecialFolder.Desktop;
            Description = string.Empty;
            SelectedPath = string.Empty;
            SelectedPathNeedsCheck = false;
            ShowNewFolderButton = true;
        }
        public new bool Multiselect { get => base.Multiselect; set => base.Multiselect = value; }
        public Environment.SpecialFolder RootFolder { get; set; } = Environment.SpecialFolder.Desktop;
        public new string SelectedPath
        {
            get => base.SelectedPath;
            set => base.SelectedPath = value;
        }
        public new  string[] SelectedPaths => base.SelectedPaths;

        public bool ShowNewFolderButton { get; set; }
        public bool SelectedPathNeedsCheck { get; set; }
        public override DialogResult ShowDialog(IWin32Window owner)
        {
            ActionType = Gtk.FileChooserAction.SelectFolder;
            return base.ShowDialog(owner);
        }
    }
}
