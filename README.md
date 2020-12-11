# GB/T 35255-2017 - LED公共照明智能系统接口应用层通信协议

## 1 概述

这是一个基于GB/T 35255-2017《LED公共照明智能系统接口应用层通信协议》的实现。本协议用于替代原 [CSA018](https://github.com/li2008kui/CSA018) 协议。

## 2 使用说明

### 2.1 准备

```
// 添加引用
using GBT35255;
using GBT35255.ProtoBase;
```

### 2.2 命令创建

```
// 创建命令
var creater = new CreateCommand(
    MessageType.Command,// 服务器与网络之间的请求命令
    0x00000001);// 网关ID

var cmd = creater.GetRequestCommand(
    MessageId.RealTimeControlLuminaire,// 实时控制
    ParameterType.Luminance,// 灯具亮度
    "100");// 亮度值

// 发送数据
```

### 2.3 命令解析

```
// 解析命令
var parser = new ParseCommand(bArray);
var datagrams = parser.DatagramList;

foreach (var datagram in datagrams)
{
    // 业务代码
}
```

## 3 标准说明

**标准号**：GB/T 35255-2017

**中文标准名称**：LED公共照明智能系统接口应用层通信协议

**英文标准名称**：Application layer communication protocols for the interface in intelligent public LED lighting systems

**标准状态**：现行

## 4 引用

[中国国家标准化管理委员会——国家标准全文公开系统](http://openstd.samr.gov.cn/bzgk/gb/newGbInfo?hcno=59482175A19132AC6B2748020ABFDA19)

[国家半导体照明工程研发及产业联盟标准化委员会](http://csas.china-led.net/)
