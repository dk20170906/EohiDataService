echo 服务EohiQuartzService卸载中..........

echo Off

sc stop EohiQuartzService

sc delete EohiQuartzService //与后台服务名称一至

echo Off

echo EohiQuartzService卸载完毕

pause
