# 触控输入信号引脚分配（使用PMOD接口）
set_property PACKAGE_PIN K1 [get_ports {touch_input[0]}]
set_property IOSTANDARD LVCMOS33 [get_ports {touch_input[0]}]
set_property PACKAGE_PIN F6 [get_ports {touch_input[1]}]
set_property IOSTANDARD LVCMOS33 [get_ports {touch_input[1]}]
set_property PACKAGE_PIN J2 [get_ports {touch_input[2]}]
set_property IOSTANDARD LVCMOS33 [get_ports {touch_input[2]}]
set_property IOSTANDARD LVCMOS33 [get_ports {touch_input[3]}]
set_property IOSTANDARD LVCMOS33 [get_ports {touch_input[4]}]
set_property IOSTANDARD LVCMOS33 [get_ports {touch_input[5]}]
set_property PACKAGE_PIN J4 [get_ports {touch_input[6]}]
set_property IOSTANDARD LVCMOS33 [get_ports {touch_input[6]}]
set_property IOSTANDARD LVCMOS33 [get_ports {touch_input[7]}]


# LED输出信号引脚分配

# 时钟和复位引脚分配
set_property PACKAGE_PIN E3 [get_ports clk]
set_property IOSTANDARD LVCMOS33 [get_ports clk]
set_property PACKAGE_PIN N17 [get_ports reset]
set_property IOSTANDARD LVCMOS33 [get_ports reset]

set_property PACKAGE_PIN G6 [get_ports {touch_input[3]}]
set_property PACKAGE_PIN E7 [get_ports {touch_input[4]}]
set_property PACKAGE_PIN J3 [get_ports {touch_input[5]}]
set_property PACKAGE_PIN E6 [get_ports {touch_input[7]}]




set_property PACKAGE_PIN E18 [get_ports tx]
set_property IOSTANDARD LVCMOS33 [get_ports tx]
