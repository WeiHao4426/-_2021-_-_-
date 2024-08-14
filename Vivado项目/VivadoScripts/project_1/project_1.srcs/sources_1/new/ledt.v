`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date: 2024/06/09 10:35:45
// Design Name: 
// Module Name: ledt
// Project Name: 
// Target Devices: 
// Tool Versions: 
// Description: 
// 
// Dependencies: 
// 
// Revision:
// Revision 0.01 - File Created
// Additional Comments:
// 
//////////////////////////////////////////////////////////////////////////////////
module top_module (
    input wire clk,
    input wire reset,
    input wire [7:0] touch_input, // 8个触摸输入引脚
    output wire tx                // UART输出引脚
);

wire [3:0] touch_output;
reg [7:0] tx_data;
reg tx_data_valid;

// 实例化触控数据处理模块
touch_processor processor (
    .clk(clk),
    .reset(reset),
    .touch_input(touch_input),
    .touch_output(touch_output)
);

reg [22:0] clock_counter; // 23-bit counter for counting up to 5000000 clock cycles
parameter CLOCK_DIVIDE = 5000000; // Adjust for 100ms interval at 100MHz clock

// 状态机
always @(posedge clk or posedge reset) begin
    if (reset) begin
        clock_counter <= 23'b0;
        tx_data <= 8'b0;
        tx_data_valid <= 1'b0;
    end else begin
        if (clock_counter == CLOCK_DIVIDE - 1) begin
            clock_counter <= 23'b0;
            tx_data <= {4'b0, touch_output}; // 扩展到8位并将其赋值给tx_data
            tx_data_valid <= 1'b1;
        end else begin
            clock_counter <= clock_counter + 1;
            tx_data_valid <= 1'b0;
        end
    end
end

// 实例化UART发送模块
uart_tx uart (
    .clk(clk),
    .rst_n(~reset),
    .tx_data(tx_data),
    .tx_data_valid(tx_data_valid),
    .tx_pin(tx)
);

endmodule

