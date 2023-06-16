# SecHex-Spoofy [1.5]

Simple HWID-Changer 🔑︎
Some Lines of the CMD Version are from @LockBlock-dev 

**Windows 11** Version 10.0.22621 Build 22621 ✅                                      
**Windows 10** Version 22H2 Build 19045.2965 ✅


# How to use
+ **Clone the Tool.**
+ **Build the Tool.**
+ **Run it as Admin.**
+ **Choose your option**


# Functions ⚡
+ **Disk Spoofing** - [18.05.23]                           
    • Retrieves SCSI ports and buses information from the Windows registry.                           
    • Checks if the device type is a disk peripheral.                           
    • Generates random identifiers and serial numbers for the disk peripheral.                           
    • Saves the before and after log information.                           
    • Updates the registry values for the disk peripheral with the new identifiers and serial numbers.                                              
+ **Guid Spoofing** - [18.05.23]                           
    • Generates new GUIDs (Globally Unique Identifiers) for various registry keys.                           
    • Updates the HwProfileGuid, MachineGuid, and MachineId registry values with new GUIDs.                           
    • Generates a random BIOS release date.                           
    • Saves the before and after log information.                                            
+ **PC-name Spoofing** - [18.05.23]                           
    • Spoofs the computer name by updating various registry keys.                           
    • Retrieves the original computer name from the registry.                           
    • Generates a random new computer name.                           
    • Updates the registry values for ComputerName, ActiveComputerName, Hostname, and • NV Hostname.                           
    • Saves the before and after log information.                                               
+ **MAC ID Spoofing** - [19.05.23]                           
    • Attempts to spoof the MAC address of network adapters.                           
    • Retrieves network adapters information from the Windows registry.                           
    • Generates a random MAC address.                           
    • Saves the before and after log information.                           
    • Updates the registry values for the MAC address with the new spoofed address.                           
    • Disables and enables the local area connection to apply the changes.                                                                                
+ **Ubisoft cache cleaner** - [19.05.23]                           
    • Cleans the Cache of Ubisoft                                                   
+ **Valorant cache cleaner** - [19.05.23]                           
    • Cleans the Cache of Ubisoft                                                 
+ **Installation ID Spoofing** - [26.05.23]
    • Attempts to spoof the Windows ID by changing the MachineGuid registry value.                           
    • Retrieves the current MachineGuid value from the registry.                           
    • Generates a new random spoofed MachineGuid.                           
    • Saves the before and after log information.                           
    • Updates the registry value with the spoofed MachineGuid.                                                   
+ **Spoof EFI Bootloader** - [26.05.23]                           
    • Opens the registry key for EFI variables.                           
    • Retrieves the current EFI Variable ID from the registry.                           
    • Generates a new random EFI Variable ID.                           
    • Updates the registry value with the new EFI Variable ID.                           
    • Saves the before and after log information.                                                       
+ **Spoof SMBIOS** - [26.05.23]                           
    • Opens the registry key for SMBIOS data.                           
    • Retrieves the current SystemSerialNumber from the registry.                           
    • Generates a new random SystemSerialNumber.                           
    • Updates the registry value with the new SystemSerialNumber.                           
    • Saves the before and after log information.                                          
+ **Get all System informations** *[New]* - [27.05.23]                           
    • Get all System informations.                                          
+ **Registry Checker** *[New]* - [07.06.23]                           
    • Defines an array of registry entries to check.                           
    • Checks if the registry keys specified in the array exist.                           
    • Creates a list of missing registry entries.                           
    • Displays an error message with the missing entries, if any.                           
    • Displays a success message if all registry entries are found.                                                   
+ **Log System** *[New]* (testing) - [10.06.23]                           
    • Log every Change in a .txt                            
+ **Backup System** *[New]* (testing) - [13.06.23]                           
    • Create a Backup as .reg                           
+ **Product ID Spoofing** *[New]* (testing) - [14.06.23]                           
    • Modifies the registry values related to the Product ID for IT purposes.                           
    • Opens the registry key "SOFTWARE\Microsoft\Windows NT\CurrentVersion" under the LocalMachine hive.                           
    • Retrieves the current value of the "ProductId" registry entry.                           
    • Generates a new random product ID using the RandomIdprid(20) method.                           
    • Sets the registry value of "ProductId" to the new generated product ID.                           
    • Logs the original and new product IDs using the SaveLogs("product", logBefore, logAfter) method.                                                    



**Design Update** - 20.05.23

# Milestones 🏆
**5 Stars** - ***EFI*** ✅                             
**10 Stars** - ***SMBIOS***  ✅                                                                 
**20+ Stars** - ***Spoofer with GUI*** ✅       
**50+ Stars** - ***GUI Changes + Code overhaul*** ✨         

▰▰▰▰▰▰▰▰▰▰▰▱▱ 85% finish ❤️


# GUI VERSION 🏆
![241434869-c401bd71-b489-4391-bcf6-231dd99353f5 (1)](https://github.com/SecHex/SecHex-Spoofy/assets/96635023/2873ea2b-6485-42ed-b448-0056f2ba7eb1)


# CMD VERSION 🏆
[No Updates for the CMD Version!]
![Screenshot 2023-05-27 232609 (2)](https://github.com/SecHex/SecHex-Spoofy/assets/96635023/c401bd71-b489-4391-bcf6-231dd99353f5)


# Support me 💸
btc bc1q8dedn4a6msjm6wf7jejmf2g47twrk6wha8u0dl                                 
eth 0xD97738045E738Cb255e2707473Dae023ef913c0A                                
bnb bnb1yxxrkwyvde9c2c8qle6d5gzm5tndyselgjd8xy                                





# Disclaimer ⚠️
Please read this disclaimer carefully before using the HWID Spoofing Tool ("Tool") available on this GitHub site. By accessing or using the Tool, you agree to be bound by this disclaimer.

Use at Your Own Risk: The HWID Spoofing Tool is provided for educational and informational purposes only. It is intended to demonstrate the concept of HWID spoofing and its potential applications. However, it is important to understand that the use of this Tool may violate the terms of service or terms of use of certain software or services. Use this Tool at your own risk.

No Guarantees: Although the HWID Spoofing Tool has been tested on a virtual machine and efforts have been made to ensure its functionality, the Tool is provided "as is" without any guarantees or warranties of any kind. I cannot guarantee that the Tool will work flawlessly on all systems or that it will not cause any issues or damages.

Limited Liability: I specifically DISCLAIM LIABILITY FOR INCIDENTAL OR CONSEQUENTIAL DAMAGES arising out of the use or inability to use the HWID Spoofing Tool. In no event shall I be liable for any loss or damage suffered as a result of the use or misuse of the Tool, including but not limited to any direct, indirect, special, or consequential damages.

Responsibility: You are solely responsible for your actions and the consequences that may arise from the use of the HWID Spoofing Tool. It is your responsibility to ensure that your use of the Tool complies with all applicable laws, regulations, and terms of service or terms of use.

Legal Implications: The use of HWID spoofing tools may be illegal or against the terms of service of certain software or services. It is your responsibility to understand and comply with the laws and terms governing the use of such tools in your jurisdiction or in relation to specific software or services.

By using the HWID Spoofing Tool, you acknowledge that you have read, understood, and agreed to this disclaimer. If you do not agree with any part of this disclaimer, do not use the Tool.
