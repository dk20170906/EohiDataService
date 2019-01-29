@echo.服务启动...... 
@echo off 
@sc create EohiQuartzService  binPath= "%~dp0\EohiQuartzService.exe" 
@net start EohiQuartzService  
@sc config EohiQuartzService  start= AUTO 
@echo off 
@echo.启动完毕！ 
@pause


