# Badge Plugin for Xamarin and Windows

### Setup
* Available on NuGet: https://www.nuget.org/packages/Xamarin.Badge.Plugin/ [![NuGet](https://img.shields.io/nuget/v/Xamarin.Badge.Plugin.svg?label=NuGet)](https://www.nuget.org/packages/Xamarin.Badge.Plugin/)
* Install into your PCL project and Client projects.

**Supports**

* Xamarin.Android
* Xamarin.iOS
* UWP
* Windows Phone 8.0
* Windows Phone 8.1 RT
* Windows Store 8.0+

Android doesn't supports app icon badge by default neither notification badge works, but third party manufacturers launchers do.

The plugin support these:

* Adw
* Apex
* Asus
* Default (because some launchers use android.intent.action.BADGE_COUNT_UPDATE to update count)
* HTC
* LG
* Nova
* Oppo
* Samsung
* Sony
* Xiaomi
* Zuk

**Android permissions**

```xml
<!-- Apex -->
<uses-permission android:name="com.anddoes.launcher.permission.UPDATE_COUNT"/>

<!-- Default -->
<uses-permission android:name="com.android.launcher.permission.READ_SETTINGS"/>
<uses-permission android:name="com.android.launcher.permission.WRITE_SETTINGS"/>
<uses-permission android:name="com.android.launcher.permission.INSTALL_SHORTCUT" />
<uses-permission android:name="com.android.launcher.permission.UNINSTALL_SHORTCUT" />

<!-- HTC -->
<uses-permission android:name="com.htc.launcher.permission.READ_SETTINGS" />
<uses-permission android:name="com.htc.launcher.permission.UPDATE_SHORTCUT" /> 

<!-- Samsung -->
<uses-permission android:name="com.sec.android.provider.badge.permission.READ" />
<uses-permission android:name="com.sec.android.provider.badge.permission.WRITE" />

<!-- Sony -->
<uses-permission android:name="com.sonyericsson.home.permission.BROADCAST_BADGE" />
<uses-permission android:name="com.sonymobile.home.permission.PROVIDER_INSERT_BADGE" />
```

### API Usage

Call **CrossBadge.Current** from any project or PCL to gain access to APIs.

**Setting application badge value**

```
CrossBadge.Current.SetBadge(10);
```

**Clearing application badge value**

```
CrossBadge.Current.ClearBadge();
```

#### Author
* [alexrainman](https://github.com/alexrainman)
