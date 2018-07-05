# Dot-plotter
Visually search for duplicate code or text

This is just a demo of dot plotting to visualise duplicated sequences in text files.

The basic idea as follows:

- tokenise a file.  The current tokeniser just treats any number of 
  non-word characters as delimiters,

- create square bitmap where the width and height are the same as the number of tokens, colour it white,

- lay the tokens along left and top edges,

- for each pixel coordinate where the token on the top edge is the same as the token on left edge colour that pixel black.

Along the diagonal will be an unbroken black line showing because those 
pixels by definition have the same token at the x and y coordinates.

There will be a scattering of other dots where the tokens coincide.  If you 
see any other top-left to bottom right diagonal lines then you have some duplicated 
code.

The current version hard codes a file name, change it to point at something interesting.