# Bluetooth

[OUIs - IEEE](http://standards-oui.ieee.org/oui.txt)

## Bluetooth Device Address (BD_ADDR)

*   LAP: The Lower Address Part of the BD_ADDR is the portion of the MAC address that is allocated by the vendor to devices.  The LAP makes up 24-bits of the BD_ADDR.  The LAP is used for uniquely identifying a Bluetooth device as part of the Access Code and synchronization word information that precedes the Bluetooth baseband header for every transmitted frame.
*   UAP: The Upper Address Part of the BD_ADDR is 8-bits of the device MAC address, representing a portion of the 24-bit prefix that is allocated to vendors by the IEEE (OUI).  The UAP is used for seeding various algorithms used in the Bluetooth specification, including the generation of the Header Error Correct (HEC) field used to identify accidentally corrupted Bluetooth packets in transit.
*   NAP: The Non-significant Address Part makes up the remaining 16 bits of the BD_ADDR information, and the remaining 16 bits of the OUI.  The NAP value is not used for any significant purposes for Bluetooth networking, other than that it is present in Frequency Hopping Synchronization frames.

![Bluetooth Device Address](/images/bdaddr.jpg)  

