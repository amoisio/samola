# Samola.Utilities

A library for general purpose utilities.

Before adding types/functionality here, read below:
- Algorithm specific functionality should go into the .Algorithms project
- Extensions should go into the .Extensions project

With custom data types think about the usage of the type. 
If any of the following conditions are met then the type should probably go in the
.DataStructures project:
- The type is used as part of another data structure
- The type is used as part of an algorithm and has a very specific, tailored, purpose

In all other cases, the .Utilities project may be the place for your type/functionality.


