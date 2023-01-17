# SkyBlazor框架是一个用于前端的Blazor框架。
本框架旨在不过多干涉项目，只需导入要求的css文件即可使用，无需在Program.cs文件中注入任何服务，也无需注入任何全局JS脚本。  

目前仅提供一个组件：  
MoveInPopover组件：响应式移入显示弹出框组件，支持高自由度设计。  

## 7.0.5版本发布更新  
可以在组件上添加额外的属性，支持自定义class和id属性。  

## 7.0.6版本发布更新  
1、新增支持属性分发功能，自由度更强大的组件更改支持！  
2、GitHub库已开源，欢迎提Issue！
3、本人QQ：867245713，有问题也可QQ咨询！

### SkyBlazor框架使用方式：
在**index.html**的<head>元素中插入以下语句：  
`<link href="_content/SkyBlazor/css/SkyBlazor.bundle.min.css" rel="stylesheet">`  
`<link href="_content/SkyBlazor/css/bootstrap/bootstrap.min.css" rel="stylesheet">`  

在根目录下的_imports.razor中加入以下语句：  
`@using SkyBlazor.Component`