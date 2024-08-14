`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date: 2024/06/09 10:34:52
// Design Name: 
// Module Name: led
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

module touch_processor (
    input wire clk,
    input wire reset,
    input wire [7:0] touch_input, // 8个触摸输入引脚
    output reg [3:0] touch_output // 输出激活触控信号的序号
);

always @(posedge clk or posedge reset) begin
    if (reset) begin
        touch_output <= 4'h0;
    end else begin
        case (touch_input)
            8'b00000001: touch_output <= 4'h0;
            8'b00000010: touch_output <= 4'h1;
            8'b00000100: touch_output <= 4'h2;
            8'b00001000: touch_output <= 4'h3;
            8'b00010000: touch_output <= 4'h4;
            8'b00100000: touch_output <= 4'h5;
            8'b01000000: touch_output <= 4'h6;
            8'b10000000: touch_output <= 4'h7;
            default: touch_output <= 4'hF; // 无效值
        endcase
    end
end

endmodule

