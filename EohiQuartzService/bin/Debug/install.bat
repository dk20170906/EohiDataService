@echo.��������...... 
@echo off 
@sc create EohiQuartzService  binPath= "%~dp0\EohiQuartzService.exe" 
@net start EohiQuartzService  
@sc config EohiQuartzService  start= AUTO 
@echo off 
@echo.������ϣ� 
@pause


