# The receiver

The voltage divider schematic
Connect the receiver’s VCC and GND pins to the Pi’s 5V and ground pins. If you’re having trouble with 5V, try the 3.3V pin, it worked better for me (less noise).
Connect one end of the line-in cable (the ground, the long bit) to the ground pin of the RasPi.
Connect the data pin of the receiver to a voltage divider, I used a 4.2 KΩ and a 1 KΩ resistor (for Z1 and Z2 in the schematic respectively) to drop the voltage from 5V to around 1V, which is what the line in expects.
Connect the output of the voltage divider to the other line-in end.
This way, your transmitter is hooked up to the 5V/GND pin, and your line-in cable is connected to the data/GND pins via the voltage drop, which gives it perfect range to read the signals the transmitter sends. You can find more details in this post, it’s about IR but it applies equally well to RF.

# The transmitter

To transmit, just connect the transmitter to the 5V/GND pins and connect the data pin to the GPIO pin you designated as LIRC output (see previous post for configuring this). That’s all there is to it, then you can use the (similarly previously) already-installed LIRC to transmit using the raw timings.
