# VMS80

VMS80-Style Cutting lathe Simulator project.

## Description

This project explores the behavior of the vinyl cutting lathe Neumann VMS80.
This model is known to have a state-of-the-art pitch control that can nest two consecutive grooves to optimize the maximal duration that can be cut on a minimal surface.

This VMS80 simulator can be used to compute the filling of the vinyl surface in a style of the Neumann model and the actual land achieved compared to a given target.

## Run the code

This repository is a Visual Studio project of a Windows Form executable.

> [!NOTE]
> NO BUILT EXECUTABLE WILL BE PROVIDED.
>
> The project needs to be built to run it.

## Dependencies

At this stage of the project, it calls an external Python script to use a dynamic plot.
This needs Python to be installed with matplotlib on the machine. 

## Structure

### *Classes* - source code files
- [Simulator.cs](VMS80/Classes/Simulator.cs) : Computes the pitch-control for the given audio based on the config file.
- [Plugin.cs](VMS80/Classes/Plugins.cs) : Controls and apply audio plugins.
    - [EllipticalFilter.cs](VMS80/Classes/EllipticalFilter.cs) : #TODO
    - [HiFreqLimiter.cs](VMS80/Classes/HiFreqLimiter.cs) : #TODO
    - [Compressor.cs](VMS80/Classes/Compressor.cs) : #TODO
- [AudioReader.cs](VMS80/Classes/AudioReader.cs) : Import audio file.
 
### *Forms* - source forms files
- [VMS80.cs](VMS80/Forms/vms80.cs) : Main Windows Form of the executable.

### *Python* - source Pyhton scripts
- [Plot.py](VMS80/Python/plot.py) : Parse the data file and plot the simulation results.

## Future steps

In order to improve the signal shape to cut into the vinyl disk, some processing can be added to limit the groove-depth (out-of-phase stereo),
the velocity and the acceleration of the stylus.

The skeleton of some of these audio processing plugins are already added into code but need to be developed in a future time.

Final graphical results are intended to be embedded int eh executable and not to call anymore an external script.

## Ressources
This work is based on the following documentation:
 * https://pspatialaudio.com/lathes.htm
 * https://www.tonmeister.ca/wordpress/2023/07/25/vinyl-info-and-calculators/
   
And is inspired from the "PLS-21 P/D" serie of [Opcode66's Youtube Channel](https://www.youtube.com/@opcode66/videos)

## Troubleshooting
Plotting of large simulation can be long.

## Contact
Open an [issue](https://github.com/Tichard/VMS80/issues) in this project.
