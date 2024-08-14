`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date: 2024/06/10 15:44:04
// Design Name: 
// Module Name: leds
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
module uart_tx (
    input wire clk,
    input wire rst_n,
    input wire [7:0] tx_data,
    input wire tx_data_valid,
    output reg tx_pin
);

parameter CLK_FRE = 100_000_000; // 100 MHz
parameter BAUD_RATE = 9600;
localparam CYCLE = CLK_FRE / BAUD_RATE;
localparam S_IDLE = 0;
localparam S_START = 1;
localparam S_DATA = 2;
localparam S_STOP = 3;

reg [1:0] state;
reg [3:0] bit_count;
reg [15:0] clk_count;
reg [7:0] data;

always @(posedge clk or negedge rst_n) begin
    if (!rst_n) begin
        state <= S_IDLE;
        bit_count <= 0;
        clk_count <= 0;
        tx_pin <= 1'b1;
    end else begin
        case (state)
            S_IDLE: begin
                if (tx_data_valid) begin
                    state <= S_START;
                    data <= tx_data;
                end
                tx_pin <= 1'b1;
            end
            S_START: begin
                if (clk_count < CYCLE) begin
                    clk_count <= clk_count + 1;
                end else begin
                    clk_count <= 0;
                    state <= S_DATA;
                    tx_pin <= 1'b0;
                end
            end
            S_DATA: begin
                if (clk_count < CYCLE) begin
                    clk_count <= clk_count + 1;
                end else begin
                    clk_count <= 0;
                    if (bit_count < 8) begin
                        tx_pin <= data[bit_count];
                        bit_count <= bit_count + 1;
                    end else begin
                        bit_count <= 0;
                        state <= S_STOP;
                    end
                end
            end
            S_STOP: begin
                if (clk_count < CYCLE) begin
                    clk_count <= clk_count + 1;
                end else begin
                    clk_count <= 0;
                    state <= S_IDLE;
                    tx_pin <= 1'b1;
                end
            end
        endcase
    end
end

endmodule
