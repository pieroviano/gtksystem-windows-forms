## Update Log

## 更新日志
 #### 2025/2/5 V1.3.24.59
	1. 更新ToolStripButton的默认显示类型
	2. 更新ToolStrip，支持最后一个菜单项右边靠
	3. ListView更新很大，实现方法增加、优化界面
	4. 更新Form，修正bug
 #### 2025/1/2 V1.3.24.58
	1. 更新容器控件排版功能。
	2. 更新datagridview数据赋值功能。
	3. 更新窗体close关闭功能。
 #### 2024/12/20 V1.3.24.57
	1. 更新listview、listbox、checkedlistbox、datagridview功能。
 #### 2024/12/14 V1.3.24.56
	1. 实现控件SizeChange事件。
	2. 更新消息框内容换行、datagridview自动换行
	3. 更新listview增删功能
	4. 修正了一些发现的问题
 #### 2024/11/7 V1.3.24.53
	1. 容器控件功能修正。
	2. 完善treeview功能
	3. 实现ImageList组件基本配置和取图功能
	4. 修正了一些发现的问题
 #### 2024/10/23 V1.3.24.51
	1. [重大更新] 容器控件功能完善和错误修正。
	2. Graphics/Image绘图功能完善
	3. 修正了一些发现的问题
 #### 2024/10/19 V1.3.24.50
	1. [重大更新] 容器布局重构，提升dock/Anchor功能，有更好的性能、多层嵌套更稳定。
	2. 实现datagridview自动换行功能和属性
	3. 增加Form置顶、激活、获取已开窗体的操作功能
	4. 修正了一些发现的问题
 #### 2024/9/28 V1.3.24.49
   1. 修正窗体paint事件，creategraphics方法
   2. 新增scrollbar、fontdailog控件
   3. 修正selectindexchanged\selectvaluechanged\selectitemchanged事件
 #### 2024/8/28 V1.3.24.48
   1. 实现所有控件右键菜单
   2. 增加主题配置、样式配置功能
 #### 2024/8/28 V1.3.24.47
   1. 调整优化控件位置边框属性精准对齐。
   2. 补充部分控件事件
 #### 2024/8/27 V1.3.24.46
   1. 修改优化窗体关闭方式，统一windows和linux窗体标题栏，提高与原生窗体关闭后重开的兼容性。
   2. DataGridView实现表格cellstyle样式功能，修改表格图片加载程序，优化性能和修正bug
   3. 优化基础数据集程序库
   4. 优化打印组件程序，增加打印预览控件和打印预览窗口
 #### 2024/7/16 V1.3.24.45
   1. 修改优化控件事件程序
 #### 2024/7/16
   1. 修正打开文件对话窗、消息弹窗
 #### 2024/7/12
   1. 增加打印组件
   2. 修复一些问题
 #### 2024/7/7
   1. 修改一些控件样式
 #### 2024/6/28
   1. 修改一些边框样式、控件鼠标事件
   2. 修改一些控件的属性值\错误
   3. 重构容器滚动窗体架构
 #### 2024/6/22
   1. 修改一些bug
   2. 修改优化窗口和控件的resize功能
 #### 2024/6/20
   1. 修改一些bug
   2. 修改优化窗口和控件的resize功能
   3. usercontrol增加鼠标事件 
 #### 2024/6/19
   1. 样式调整
   2. 修改一些bug
   3. 优化窗口和控件的resize功能
   4. 实现控件的位置属性、大小可调
   5. 实现部分控件鼠标样式属性

#### 2024/6/10
1. Improved the background image display mode for controls, adding circular and transparent background image support (important).
2. Optimized the functionality and performance of many controls.
3. Enhanced style display programs for controls, supporting theme switching.
4. Fixed some discovered functional or program errors.
5. Added asynchronous image loading for `DataGridView` URLs and optimized `DataGridView` data display performance.

#### 2024/5/30
1. **[Important]** Fixed threading issues for UI updates, including `Timer` execution synchronization.
2. Fixed background colors for `ListBox`, `ListView`, and `RichTextBox`.
3. Added project demonstration examples for changing navigation data displays.

#### 2024/5/28
1. Fixed and added some methods for controls.
2. Introduced multi-threaded UI update programs for GTK.

#### 2024/5/21
1. Added functionality for `ComboBox` and `ListBox`.
2. Fixed issues with `ToolStripSeparator`.

#### 2024/5/17
1. Added time data and format modes for `DateTimePicker`.

#### 2024/5/16
1. Fixed issues with border lines in forms.
2. Added some frequently used control properties.
3. Enhanced the `ComboBox` control with selectable `DropDown` and `DropDownList` modes.
4. Improved functionality for Visual Studio plugin features and installation compatibility.

#### 2024/5/11
1. Fixed size anomalies for forms when starting.
2. Added `Image` property for buttons.
3. Fixed background positions for controls.

#### 2024/5/6
1. Completed functionality for `TreeView` and `ListView`.
2. Released the development tool `GTKSystem.Windows.FormsDesigner.dll` (NuGet installation) for auto-detection and correction of form designer configurations during compilation.
3. Fixed data retrieval errors in `DataGridView`.

#### 2024/5/1
1. **[Major Update]** Redesigned the structure of control programs, improving functionality and fixing errors.
2. Optimized rendering and background programs for controls to prevent child controls from overlapping background elements.
3. Specifically enhanced form layout programs and features.

#### 2024/4/20
1. Fixed positioning issues in `Graphics` rendering.
2. Implemented `GraphicsPath` rendering with gradient colors.
3. Added `BeginInvoke` and `EndInvoke` methods for controls.
4. Improved `DataGridView` and `ListBox` data loading programs.

#### 2024/3/27
1. Fixed issues with `UserControl` failing to display in the form designer.

#### 2024/3/19
1. Fixed panel content scrolling display anomalies and optimized form resizing programs.

#### 2024/3/14
1. Fixed issues with `TreeView` data loading programs.

#### 2024/3/6
1. Corrected form configuration and binding issues.

#### 2024/3/2
1. Fixed font size issues for `Label` text and added alignment properties.
2. Enabled the use of `ImageList` in the form designer.

#### 2024/2/29
1. Added dashed lines and multi-sided shapes to `Graphics` rendering and optimized text rendering programs.
2. Fixed some known bugs.

#### 2024/2/23
1. Implemented and fixed `DataGridView` cell data editing and retrieval functionalities.
