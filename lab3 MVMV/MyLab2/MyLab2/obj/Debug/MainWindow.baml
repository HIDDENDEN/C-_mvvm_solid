   M S B A M L   `   `   `  ����A  =MyLab2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ViewView  5   	   D @ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null8*clr-namespace:ViewModel;assembly=ViewModel	ViewModel 5          View.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:View     5   	   =local1*clr-namespace:ViewModel;assembly=ViewModel     5      -   5        $��
Lab2Window��5   	     � Title$  
MainWindow��5
   	   $	��825��6   $
��1220��6)    � 	Resources 5         ViewModel.MDStaticResClass   	modelData  ViewModel.MyConverter  MyConverter  ModelDataTemplate �� Orientation �� Path �� 	Converter��%�   &	        &	      &	 &     7    5   
   6"     5   
   6   �� 5   
   .R�5      �� $ Vertical=�6   .+�5      �� $ 
Horizontal=�6   .+�5      �� ��6    �� $ .�8   5      5      5      5      .��5      � .+�5   
   �� -   5        $��MainListBox��5   M   $
��Left=�6   $	��279��6-   $��	10,25,0,0q�6:   $	��Top=�6`   $	��337��5      #��[ 5      ��5      �� $ obs_md� �� SelectedIndex 5      �� $ ListBoxSelectedIndex� �� Mode$ TwoWay=�o� 5   
   $
��Left=�6   $��20��6*   $	��Top=�66   $	��793��6N   .@�5      m� $
��File��6   .@�5       m� $	��New��6    m� Command 6(   �� *
NewCommand+m� 5!      $
��Open��6    6)   �� *OpenCommand+m� 5"      $
��Save��6    6)   �� *SaveCommand+5#      m� 5$      $
��Edit��6   .@�5%      m� $	��Add��6    6+   �� *
AddCommand+m� 5&      $��Remove��6    6+   �� *DeleteCommand+5'      m� 5(      $
��Draw��6    6%   �� *DrawCommand+5)      �� 5*   
   -   5        $"��NumOfDigitsAfterCommaTextBox��5*   *   
�������$
��Left=�6O   $��52��6j   $��227,434,0,0q�6v    �� TextWrapping$
 Wrap=�5+      $	��Top=�5,      $	��120��6+   ��5+   '   �� $ Num_of_digits_after_comma�	 �� ValidatesOnDataErrors	 . $ TwoWay=�5,   +   �� 5-   
   -   5        $��ParamTextBox��5-   .   
����İ�$
��Left=�6B   $��38��6]   $��227,321,0,0q�6i   $
 Wrap=�6~   $	��Top=�5.   Y   $	��120��6q   ��6   �� $ MyMD.p�	 . $ TwoWay=�6q   �� 5/   
   -   5        $��NumOfGridNodesTextBox��5/   )   
����ح�$
��Left=�6F   $��44��6a   $��227,372,0,0q�6m   $
 Wrap=�6�   $	��Top=�50   i   $	��120��6�   ��6   �� $ MyMD.num_of_grid_nodes�	 . $ TwoWay=�6�   �� 52   
   $
��Left=�6   $��227,521,0,0q�6.   $	��Top=�6C   $	��120��6[   $
 �� IsSynchronizedWithCurrentItem$

 TrueY�54       �� SelectedValuePath$  ��55      ��53      �� $ Types_of_graphs� �� SelectedItem 56      �� $ Type_of_diagram�7  1System.Windows.Forms.Integration.WindowsFormsHost  58   
   -   5        $��windowsformsHost��58      $
��Left=�59      $	��632��6(   $��
377,66,0,0q�65   $	��Top=�6I   $	��826��6a   �� 5:   
   
�������$
��Left=�6+   $��52��6F   $��
54,434,0,0q�6R   $
��Wrap=�6f   $	��Top=�6z   $	��120��6�   ��5;      `� 5<       �� StringFormat$2 ,Amount of digits after comma from {0} to {1}��6   .1�5=      ��  �� ElementName$ 
Lab2Window��6   $1 +modelDataView.Num_of_digits_after_comma_Min�67   �� 5>      $ 
Lab2Window��6   $1 +modelDataView.Num_of_digits_after_comma_Max�67   5?      5A      �� 5B   
   $
��Left=�6   $��16��6/   $��
54,527,0,0q�6;   $
��Wrap=�6O   $��Diagram type��6c   $	��Top=�6w   $	��120��6�   �� 5C   
   
����ح�$
��Left=�6+   $��44��6F   $��
54,372,0,0q�6R   $
��Wrap=�6f   $	��Top=�6z   $	��120��6�   ��5D      `� 5E      $J Dnumber of points                                in range [{0} ; {1}]��6   .1�5G      ��  �� Source# [  6   $
 nMin�6B   �� 5H      # [  6   $
 nMax�6B   5I      5K      �� 5L   
   
����İ�$
��Left=�60   $��38��6K   $��
54,321,0,0q�6W   $
��Wrap=�6k   $	��Top=�6   $	��120��6�   ��5M      `� 5N      $? 9p value                               in range[{0} ; {1}]��6   .1�5P      �� # [  6   $
 pMin�6B   �� 5Q      # [  6   $
 pMax�6B   5R      5T      c� 5U   
   
����� �$
��Left=�6-   $��36��6H   $��
377,25,0,0q�6T   $	��Top=�6h   $	��534��6�   .@�5V      �� ��6   �� *OpenCommand+.��5W      �� $ 
Horizontal=�6   .+�5X      �� 
�������$��Right=�68   $��50��6T   $��5 0 0 0q�6_   Open6v   5Y      5Z      �� 5[      ��6   �� *SaveCommand+.��5\      �� $ 
Horizontal=�6   .+�5]      �� 
�������$��Right=�68   $��50��6T   $��6 0 0 0q�6_   Save6v   5^      5_      �� 5`      ��6   �� *
AddCommand+.��5a      �� $ 
Horizontal=�6   .+�5b      �� 
�������$��Right=�68   $��50��6T   $��6 0 0 0q�6_   Add6u   5c      5d      �� 5e      ��6   �� *DeleteCommand+.��5f      �� $ 
Horizontal=�6   .+�5g      �� 
�������$��Right=�68   $��50��6T   $��6 0 0 0q�6_   Remove6x   5h      5i      �� 5j      ��6   �� *DrawCommand+.��5k      �� $ 
Horizontal=�6   .+�5l      �� 
�������$��Right=�68   $��50��6T   $��6 0 0 0q�6_   Draw6v   5m      5n      5o      5p      5q      