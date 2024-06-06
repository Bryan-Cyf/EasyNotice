# EasyNotice

## Nuget包

| Package Name |  Version | Downloads |
|--------------|  ------- | ---- |
| EasyNotice.Core | [![NuGet](https://img.shields.io/nuget/v/EasyNotice.Core)](https://www.nuget.org/packages/EasyNotice.Core) | [![NuGet](https://img.shields.io/nuget/dt/EasyNotice.Core)](https://www.nuget.org/packages/EasyNotice.Core) |
| EasyNotice.Dingtalk | [![NuGet](https://img.shields.io/nuget/v/EasyNotice.Dingtalk)](https://www.nuget.org/packages/EasyNotice.Dingtalk) | [![NuGet](https://img.shields.io/nuget/dt/EasyNotice.Dingtalk)](https://www.nuget.org/packages/EasyNotice.Dingtalk) |
| EasyNotice.Email | [![NuGet](https://img.shields.io/nuget/v/EasyNotice.Email)](https://www.nuget.org/packages/EasyNotice.Email) | [![NuGet](https://img.shields.io/nuget/dt/EasyNotice.Email)](https://www.nuget.org/packages/EasyNotice.Email) |
| EasyNotice.Feishu | [![NuGet](https://img.shields.io/nuget/v/EasyNotice.Feishu)](https://www.nuget.org/packages/EasyNotice.Feishu) | [![NuGet](https://img.shields.io/nuget/dt/EasyNotice.Feishu)](https://www.nuget.org/packages/EasyNotice.Feishu) |
| EasyNotice.Weixin | [![NuGet](https://img.shields.io/nuget/v/EasyNotice.Weixin)](https://www.nuget.org/packages/EasyNotice.Weixin) | [![NuGet](https://img.shields.io/nuget/dt/EasyNotice.Weixin)](https://www.nuget.org/packages/EasyNotice.Weixin) |

---------

# `EasyNotice`
> 这是一个基于.NET开源的消息通知组件，它包含了邮件通知、钉钉、飞书、企业微信通知，可以帮助我们更容易地发送程序异常通知！

-------

## 功能介绍
 - 支持[邮件]、[钉钉]、[飞书]、[企业微信]方式发送
 - 支持自定义发送间隔，避免同样的异常频繁通知
 - 傻瓜式配置，开箱即用

## 平台支持
- [x] SMTP邮箱
- [x] 钉钉群机器人
- [x] 飞书群机器人
- [x] 企业微信群机器人

## 文档资料
- [x] [『EasyNotice』项目接入文档](https://chenyuefeng.blog.csdn.net/article/details/129963124)
 ---------

# 项目接入
> 项目接入提供了以下发送方式的Demo：邮件通知、钉钉通知、飞书、企业微信通知

## 1. 邮件通知
> 邮件通知支持同时发送给多个收件人
### Step 1 : 安装包，通过Nuget安装包

```powershell
Install-Package EasyNotice.Core
Install-Package EasyNotice.Email
```

### Step 2 : 配置 Startup 启动类

```csharp
public class Startup
{
    //...
    
    public void ConfigureServices(IServiceCollection services)
    {
        //configuration
        services.AddEasyNotice(config =>
        {
            config.IntervalSeconds = 10;//同一标题的消息，10秒内只能发一条，避免短时间内大量发送重复消息
            config.UseEmail(option =>
            {
                option.Host = "smtp.qq.com";//SMTP地址
                option.Port = 465;//SMTP端口
                option.FromName = "System";//发送人名字（自定义）
                option.FromAddress = "12345@qq.com";//发送邮箱
                option.Password = "passaword";//秘钥
                option.ToAddress = new List<string>()//收件人集合
                {
                    "12345@qq.com"
                };
            });
        });
    }    
}
```

### Step 3 : IEmailProvider服务接口使用

```csharp
[ApiController]
[Route("[controller]/[action]")]
public class NoticeController : ControllerBase
{
    private readonly IEmailProvider _mailProvider;
    public NoticeController(IEmailProvider provider)
    {
        _mailProvider = provider;
    }

    [HttpGet]
    public async Task SendMail([FromQuery] string str)
    {
        await _mailProvider.SendAsync(str, new Exception(str));
    }
}
```

---------


## 2. 钉钉通知
> [配置钉钉群机器人官方文档](https://developers.dingtalk.com/document/app/custom-robot-access)

### Step 1 : 安装包，通过Nuget安装包

```powershell
Install-Package EasyNotice.Core
Install-Package EasyNotice.Dingtalk
```

### Step 2 : 配置 Startup 启动类

```csharp
public class Startup
{
    //...
    
    public void ConfigureServices(IServiceCollection services)
    {
        //configuration
        services.AddEasyNotice(config =>
        {
            config.IntervalSeconds = 10;//同一标题的消息，10秒内只能发一条，避免短时间内大量发送重复消息
            config.UseDingTalk(option =>
            {
                option.WebHook = "https://oapi.dingtalk.com/robot/send?access_token=xxxxx";//通知地址
                option.Secret = "secret";//签名校验
            });
        });
    }    
}
```

### Step 3 : IDingtalkProvider服务接口使用

```csharp
[ApiController]
[Route("[controller]/[action]")]
public class NoticeController : ControllerBase
{
    private readonly IDingtalkProvider _dingtalkProvider;
    public NoticeController(IDingtalkProvider dingtalkProvider)
    {
        _dingtalkProvider = dingtalkProvider;
    }

    [HttpGet]
    public async Task SendDingTalk([FromQuery] string str)
    {
        await _dingtalkProvider.SendAsync(str, new Exception(str));
    }
}
```

---------

## 3. 飞书通知
> [配置飞书群机器人官方文档](https://open.feishu.cn/document/ukTMukTMukTM/ucTM5YjL3ETO24yNxkjN?lang=zh-CN)

### Step 1 : 安装包，通过Nuget安装包

```powershell
Install-Package EasyNotice.Core
Install-Package EasyNotice.Feishu
```

### Step 2 : 配置 Startup 启动类

```csharp
public class Startup
{
    //...
    
    public void ConfigureServices(IServiceCollection services)
    {
        //configuration
        services.AddEasyNotice(config =>
        {
            config.IntervalSeconds = 10;//同一标题的消息，10秒内只能发一条，避免短时间内大量发送重复消息
            config.UseFeishu(option =>
            {
                option.WebHook = "https://open.feishu.cn/open-apis/bot/v2/hook/xxxxx";//通知地址
                option.Secret = "secret";//签名校验
            });
        });
    }    
}
```

### Step 3 : IFeishuProvider服务接口使用

```csharp
[ApiController]
[Route("[controller]/[action]")]
public class NoticeController : ControllerBase
{
    private readonly IFeishuProvider _feishuProvider;
    public NoticeController(IFeishuProvider feishuProvider)
    {
        _feishuProvider = feishuProvider;
    }

    [HttpGet]
    public async Task SendFeishu([FromQuery] string str)
    {
        await _feishuProvider.SendAsync(str, new Exception(str));
    }
}
```

---------

## 4. 企业微信通知
> [配置企业微信群机器人官方文档](https://developer.work.weixin.qq.com/document/path/91770)

### Step 1 : 安装包，通过Nuget安装包

```powershell
Install-Package EasyNotice.Core
Install-Package EasyNotice.Weixin
```

### Step 2 : 配置 Startup 启动类

```csharp
public class Startup
{
    //...
    
    public void ConfigureServices(IServiceCollection services)
    {
        //configuration
        services.AddEasyNotice(config =>
        {
            config.IntervalSeconds = 10;//同一标题的消息，10秒内只能发一条，避免短时间内大量发送重复消息
            config.UseWeixin(option =>
            {
                option.WebHook = "https://qyapi.weixin.qq.com/cgi-bin/webhook/send?key=xxxxx";//通知地址
            });
        });
    }    
}
```

### Step 3 : IWeixinProvider服务接口使用

```csharp
[ApiController]
[Route("[controller]/[action]")]
public class NoticeController : ControllerBase
{
    private readonly IWeixinProvider _weixinProvider;
    public NoticeController(IWeixinProvider weixinProvider)
    {
        _weixinProvider = weixinProvider;
    }

    [HttpGet]
    public async Task SendWexin([FromQuery] string str)
    {
        await _weixinProvider.SendAsync(str, new Exception(str));
    }
}
```

---------
## 更多示例

1. 查看 [更多使用例子](https://github.com/Bryan-Cyf/EasyNotice/tree/master/sample)
2. 查看 [更多测试用例](https://github.com/Bryan-Cyf/EasyNotice/tree/master/test)

