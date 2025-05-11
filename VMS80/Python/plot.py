#!/usr/bin/env python3
import os
import math
import matplotlib.pyplot as plt
 
# Select Display
DISPLAY_PITCH = 0
DISPLAY_ERR = 0
DISPLAY_POLAR = 1

audio_array = []
groove_array = []
pitch_array = []
raw_array = []
land_array = []
inner_x_array = []
outer_x_array = []
inner_y_array = []
outer_y_array = []

the_filename = os.path.join(os.path.dirname(__file__),"pitch.data")

the_file = open(the_filename, "r")
the_lines = the_file.read().splitlines()

the_line = the_lines[0].replace(',','.').split(' ')
start = int(the_line[0])
revolution_len = int(the_line[1]) + 1

the_lines.pop(0)
for the_line in the_lines:
	the_line = the_line.replace(',','.').split(' ')
	data_l = float(the_line[0])
	data_r = float(the_line[1])
	pitch = float(the_line[2])
	outer = float(the_line[3])
	inner = float(the_line[4])
	raw = float(the_line[5])
	polar = float(the_line[6])
	land = float(the_line[7])

	outer_groove = start - pitch - outer
	inner_groove = start - pitch - inner

	audio_array.append([data_l, data_r])
	groove_array.append([outer_groove, inner_groove])
	pitch_array.append(pitch)
	raw_array.append(raw)
	land_array.append(land)

	# Recreate polar project to be zoomable
	inner_x_array.append(inner_groove * math.cos(polar))
	inner_y_array.append(inner_groove * math.sin(polar))
	outer_x_array.append(outer_groove * math.cos(polar))
	outer_y_array.append(outer_groove * math.sin(polar))

if DISPLAY_PITCH :
	plt.figure(1)
	plt.subplot(2,1,1),
	plt.plot(raw_array)
	plt.plot(pitch_array)

	plt.subplot(2,1,2),
	plt.plot(audio_array)

if DISPLAY_ERR :
	plt.figure(2)
	plt.subplot(2,1,1),
	plt.plot(groove_array[0:-revolution_len])
	plt.plot(groove_array[revolution_len:-1])

	plt.subplot(2,1,2),
	plt.plot(land_array[0:-revolution_len])

if DISPLAY_POLAR :
	plt.figure(3)
	plt.plot(inner_x_array, inner_y_array)
	plt.plot(outer_x_array, outer_y_array)

plt.show()

print("DONE")