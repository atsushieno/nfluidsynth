# What is this?

nfluidsynth is a C# binding for [fluidsynth](https://github.com/Fluidsynth/fluidsynth/).

It is a P/Invoke wrapper, therefore you need native libfluidsynth.so / libfluidsynth.dylib / (lib)fluidsynth.dll.
nfluidsynth builds and packages don't come up with those native libraries, so you are supposed to prepare them by yourself (at least for now).

The target API is fluidsynth 2.0.x. The API mappings may not be complete (contributions are welcome).

For real-world-ish use case, see [fluidsynth-midi-service](https://github.com/atsushieno/fluidsynth-midi-service).

[soundfont-player-cs](https://github.com/atsushieno/soundfont-player-cs) might become another use case but it is very immature private hack.
