# Dragon_Quest_II_Nintendo_Switch_Text_Editor

![Title](https://github.com/MrVtR/Dragon_Quest_II_Nintendo_Switch_Text_Editor/blob/main/images/title_1.png)
![Subtitle](https://github.com/MrVtR/Dragon_Quest_II_Nintendo_Switch_Text_Editor/blob/main/images/subtitle1_en.png)

## Project concept
Repository that contains the code for edit text from stringDataEn.bin and stringMapEn.bin files from DQ II - Nintendo Switch Version (Tested only on StringDataEn.bin from DQ II yet), probably it'll work on DQ I and DQ III with some minor adjustment, I'll work on those two games too
  
## Prerequisites and resources used
C# programming language was used to create the script, along with the UI interface
  
## Step by step
1. I studied the stringDataEn.bin file in its hexadecimal aspects, along with concept tests on Nintendo Switch consoles, to verify that changes made manually showed up on the console
2. After this, I created a tool that shows the texts of the game formatted as a Search Tree, this way, it is possible to see the whole text structure in a more visual way,and to edit the texts using the official font of the game

## Execution
To test the program, you need to compile the project preferably with Visual Studio IDE, after that you need the file stringDataEn.bin from the game Dragon Quest II for Nintendo Switch (No details on how to extract this text file from the game or how to download it will be provided)

### To-Do List for Dragon Quest II
- [x] Create StringDataEn.bin extractor
- [x] Include extracted data from StringDataEn.bin to TreeView to support text editions
- [ ] Create save function to repack StringDataEn.bin
- [ ] Create extraction function for StringMapEn.bin
- [ ] Include extracted data from StringMapEn.bin to TreeView to support text editions
- [ ] Create save function to repack StringMapEn.bin
- [ ] Create Find Text function

## Authors
* Vítor Ribeiro ([MrVtR](https://github.com/MrVtR))

## Images/screenshots

![Imagem](https://github.com/MrVtR/Dragon_Quest_II_Nintendo_Switch_Text_Editor/blob/main/images/img.png)
---

![Imagem](https://github.com/MrVtR/Dragon_Quest_II_Nintendo_Switch_Text_Editor/blob/main/images/img2.png)

