﻿@echo 请稍等，EohiQuartzService服务安装启动中............
@echo off
@title 安装windows服务：EohiQuartzService
@sc create EohiQuartzService binPath= "%~dp0\EohiQuartzService.exe"  //服务启动文件的地址，
@sc config EohiQuartzService start= auto //启动方式为自动
@sc start EohiQuartzService     //安装后就启动服务
@echo.EohiQuartzService启动完毕
@pause