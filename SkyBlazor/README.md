# SkyBlazor框架是一个用于前端的Blazor框架。
目前仅提供一个组件：  
MoveInPopover组件：响应式移入显示弹出框组件，支持高自由度设计。


### SkyBlazor框架使用方式：
在**index.html**的<head>元素中插入以下语句：  
(```)
<link href="_content/SkyBlazor/css/SkyBlazor.bundle.min.css" rel="stylesheet">
<link href="_content/SkyBlazor/css/bootstrap/bootstrap.min.css" rel="stylesheet">
(```)

在根目录下的_imports.razor中加入以下语句：  
`@using SkyBlazor.Component`