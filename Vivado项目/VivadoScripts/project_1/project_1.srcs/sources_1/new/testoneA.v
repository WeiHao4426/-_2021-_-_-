`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date: 2024/05/31 08:48:08
// Design Name: 
// Module Name: testoneA
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


module ttp226_touch (
    input wire clk,
    input wire reset,
    input wire dv,               // 数据有效信号
    input wire [7:0] touch_input, // 8个触摸输入引脚
    output reg [7:0] touch_data,
    output reg valid
);

always @(posedge clk or posedge reset) begin
    if (reset) begin
        touch_data <= 8'b0;
        valid <= 1'b0;
    end else if (dv) begin
        touch_data <= touch_input;
        valid <= 1'b1;
    end else begin
        valid <= 1'b0;
    end
end

endmodule

