   M S B A M L   `   `   `  ����C  ?MargaDev, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:Group5Group5  5   	       Group5.RoomManagement   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Group5     5   	     � Title$  Configurar Habitaciones��5   	   $	��350��6)   $	��750��66   $	��350��6B   $	��350��6R   $	��750��6b   $	��750��6q   .��5	      � ���	#FF990000.+�5
   
   �� -   5         ��Name$ lblTitle��5
      $��Configurar Habitaciones��6"   $��
11,6,291,0q�6D   $	��Top=�6X   $
��Bold"�6p   $��36(�6�   $��69��6�   
�������6�   �� 5   
   -   5        $ lblInstructions��5      $k��eEsta ventana le permite al usuario administrar 
El precio y la cantidad de cada tipo de habitación.��6)   $��10.875,61,243.483,0q�6�   $	��Top=�6�   $��78��6�   
�������$��18(�6�   �� 5   
   -   5        $ lblRoomType��5      $��Tipo de Cuarto��6%   $
��Left=�6>   $��
11,127,0,0q�6Y   $	��109��6m   $��14(�6y   
�������$��32��6�   $	��Top=�6�   �� 5   
   -   5         ��Name$ cbxRoomType��5      $
��Left=�6(   $��148.531,131.975,0,0q�6C   $��196.388��6`    �� SelectedIndex$ 0��6p   $��25��6�   $	��Top=�6�   $��1��6�   .@�5      �� $"��Seleccione un tipo de cuarto��6   $��14(�6B   �� 5      $��One King��6   $��14(�6.   �� 5      $��One King Deluxe��6   $��14(�65   �� 5      $��
Two Queens��6   $��14(�60   �� 5      $��Two Queen Deluxe��6   $��14(�66   �� 5      $��One King Suite��6   $��14(�64   �� 5      $!��One King Presidential Suite��6   $��14(�6A   5      �� 5   
   -   5        $ lblPrice��5      $��Precio                  $��6"   $
��Left=�6F   $��11,0,0,127.8q�6a   $��Bottom=�6w   $��26��6�   $	��130��6�   $��14(�6�   
�������6�   �� 5   
   -   5         ��Name$ txtPrice��5      $
��Left=�6$   $��27��6?   $��149,0,0,125.8q�6K    �� TextWrapping$
 Wrap=�6b   $��Bottom=�6v   $	��196��6�   $��14(�6�   $��Center=�6�   $��2��6�   �� 5   
   -   5        $ lblQuantity��5      $��Cantidad��6%   $
��Left=�68   $��11,0,0,81.8q�6S   $��Bottom=�6h   $��37��6�   $��83��6�   $��14(�6�   
�������6�   �� 5   
   -   5        $ txtQuantity��5      $
��Left=�6'   $��26��6B   $��148.531,0,0,90.695q�6N   $
 Wrap=�6j   $��Bottom=�6~   $��196.388��6�   $��14(�6�   $��Center=�6�   $��3��6�   �� 5   
   -	   5         ��Name$ 	btnSubmit��5      $��Enviar��6$   $��10.875,0,0,22.667q�65   $��Bottom=�6P   $��40��6k   $��14(�6w   
�������
���   �$
��Bold"�6�   $
��Left=�6  $	��130��6  �� 5   
   -
   5        $ btnClear��5      $��Limpiar��6#   $
��Left=�65   $��214.919,0,0,22.667q�6P   $��Bottom=�6l   $	��130��6�   $��40��6�   $��14(�6�   
�������
���   �$
��Bold"�6�   6�   �� 5   
   -   5        $ btnReturntoMainMenu��5      $��Regresar al Menu��6.   $��Right=�6I   $��	0,24,29,0q�6e   $	��Top=�6x   $	��160��6�   $��40��6�   
�������
���   �$
��Bold"�6�   $��14(�6  &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 
dtgResults��5      $��0,131.975,29,22.667q�6'      
IsReadOnly . $��Right=�6V   $��327.881��6r   	 J� IsHitTestVisible	 .  ��.  
   AutoGenerateColumns
 .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Tipo de Cuarto��6e   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *
RoomType+   ClipboardContentBinding 6B   Z� 6e     5       $ Precio��6w    6%   �� *	RoomPrice+ �� StringFormat$ C2�� 6T   Z� 6w     5!      $ Cantidad��6i    6%   �� *RoomQuantity+ 6F   Z� 6i   5#      5%      5&      