# Robotic IoT

System Design
===============
The Robotic IoT design allows for quick creation of prototype systems or complete IoT solutions. 
It enables easy integration with cloud services but also allows for a local installation in entirely private infrastructure, 
with no external dependencies.

The system architecture is composed of three main layers: presentation, abstraction management, device management.
The interaction between the end user and the system is posible due to the logic of the presentation layer. Its function is to provide access to services, display data, and process user requests.
User can interact with the presentation layer through a web page, through the RT Middleware or own custom layer using HTTP or MQTT.

Core logic of the system is implemented in the management abstraction layer. It is responsible for interpreting requests from end user or presentation layer, storing the abstract system state and coordination physical devices management layer.

Device management layer has device register device, accepts requests to perform a specific action by the edge device controller. 
Edge device controller is a module responsible for direct setting and getting values on a physical device.

Connecting, Control, Monitoring and Automation
===============
<img src="https://raw.githubusercontent.com/rachwal/IoT/master/img/IoT_Design.png" alt="IoT System Design"/>
<br/>

Home Lights application
---------------
<img src="https://raw.githubusercontent.com/rachwal/IoT/master/img/IoT_Home_Lights.png" alt="IoT Home Lights Application"/>
[![Screenshot](https://raw.githubusercontent.com/rachwal/IoT/master/img/IoT_Home_Lights_screenshot.png)](https://vimeo.com/181041146)

Integration with an inteligent platform - RT Middleware
===============

Dominant Color application
---------------
<img src="https://raw.githubusercontent.com/rachwal/IoT/master/img/IoT_RT_Middleware.png" alt="IoT RT Middleware Application"/>
[![Screenshot](https://raw.githubusercontent.com/rachwal/IoT/master/img/IoT_RT_Middleware_screenshot.png)](https://vimeo.com/180534024)

Edge Devices Integration - [Wio Link](https://github.com/Seeed-Studio/Wio_Link) Environment
===============
<img src="https://raw.githubusercontent.com/rachwal/IoT/master/img/IoT_Devices.png" alt="IoT Devices"/>