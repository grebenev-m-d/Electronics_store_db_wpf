
INSERT INTO [role] (id, [name])
VALUES ('6CAD03F4-CD49-4D87-8FA2-33447F6ABB9F', 'admin');
  INSERT INTO [role] (id, [name])
VALUES ('88BA2BD7-9930-4B61-BF54-379B641C898E', 'cashier');
  INSERT INTO [role] (id, [name])
VALUES ('B6BFE182-C601-4EFC-AB3A-3EF9E68E817B', 'manager');

INSERT INTO employee (id, surname, first_name, patronymic, position, image, salary, email, phone, Birthday, passwordhash, role_id)
SELECT '1f5c72ab-11f5-4b23-b9b9-bf313d74e7b7', 'Smith', 'John', 'Michael', 'Manager', 'Resources\Img\Employees\manager.jpg', 60000.00, 'john.smith@example.com', '86756499837', '1950-02-01' ,'$2a$11$n2vxkmuHuN6u6tkDR7fFLezOvn9hGu1L14YwVrvk/tbEQ/ajMfZm6', 'B6BFE182-C601-4EFC-AB3A-3EF9E68E817B'
UNION ALL
SELECT 'eab9a274-3c83-4968-94cd-6da3d5d69ef4', 'Johnson', 'Emily', 'Grace', 'Cashier', 'Resources\Img\Employees\cashier.jpg', 30000.00, 'emily.johnson@example.com', '83747376251', '1960-01-31' , '$2a$11$jhA5COHTojH/jo/x2MrTauUDL61C6N5NcAwJte2Rv3kNmCxklI4Z2', '88BA2BD7-9930-4B61-BF54-379B641C898E'
UNION ALL
SELECT 'ef7cea96-59f0-43cd-9984-893baa4d3d0f', 'Brown', 'Michael', 'James', 'Admin', 'Resources\Img\Employees\admin.jpg', 50000.00, 'michael.brown@example.com', '83746523544', '1924-06-04' , '$2a$11$IbN545mqS5z95UuvNYqPoenm0I72WDAjIFhzgm9J1P9KuwHfPUR06', '6CAD03F4-CD49-4D87-8FA2-33447F6ABB9F';

INSERT INTO category (id, name) VALUES
   ('9eb9d73e-45fb-47a6-8fff-e1c42c7c552d', '���������'),
   ('4637328f-ce92-48fc-8138-5cbc93b0fe40', '��������'),
   ('eb50f925-3a67-4293-8f1b-60fe2732b465', '����������'),
   ('25293199-5ca9-48c6-9168-9f91be142f05', '��������'),
   ('ee043a6f-29a9-4a83-963c-dbcdd52a911b', '������'),
   ('b87bc505-208b-4789-8062-e12c7ce29e0c', '������� �������'),
   ('8d63c1db-d221-4a18-ba7c-305908d93a8b', '����������');

INSERT INTO [client] (id, [surname], [first_name], [patronymic], [email], [phone], Birthday)
VALUES ('7728F6BB-9F31-4648-9093-A3E0C657B7C0', '���������', '��������', '��������', 'user2@example.com', '89208532484', '1970-02-05'),
 ('69D933AB-45E6-4C1E-87F3-B8DBC5DA391C', '��������', '����������', '��������', 'user3@example.com', '89207963484', '1953-06-01'),
 ('F81FEB5F-D319-4C77-8DB5-BA1ACFFA935D', '���������', '�����', '��������', 'user5@example.com', '89208563765', '1937-02-01'),
 ('57406836-11F8-4C17-9FE9-BA6908226FB7', '������', '������', '���������', 'user1@example.com', '89208563995', '1957-02-04'),
 ('6D625AF7-BC56-4345-899D-BA8C67A59171', '���������', '������', '��������', 'user4@example.com', '89208529845', '1999-02-01');

INSERT INTO product (id, name, price, image, description, quantity, category_id)
VALUES ('12533992-11da-4579-95b1-0c32dec8e741', '��������� LED Xiaomi MI TV A2 43', 25000.00, 'Resources\Img\Product\��������� LED Xiaomi MI TV A2 43.jpg', '��������� LED Xiaomi MI TV A2 43 � ������� ������ ������� �� ������� ������� ������� Direct LED ���������� 108 ��. ��������� ����� ����������� ���������� ����������� ������������. ���������� ������ ���������� 3840x2160 ����., ��� ������������� ������� 4K UltraHD. ���������� Dolby Vision, HDR10 � HLG �������� �� ��������� �������������������� �������� �����������.', 32, 'eb50f925-3a67-4293-8f1b-60fe2732b465'),
('99790ed4-3467-45f9-a16b-263b1f6d4348', 'Sony WH-1000XM4', 37000.00, 'Resources\Img\Product\Sony WH-1000XM4.jpg', '��������������� ������������ 2.0 Bluetooth-��������� Sony WH-1000XM4 ������������� �� ������������� ������. ������������ ����������� ���������� ������������ ���������� ����� �������� ����. ����� �� �������� ��� ��������� ��������������� �� ������������� ������� ���������� � ���������� ���������. ��������� �������� ��������� � ������������� �������� ��������� ����� ������. ���� ��������������� � ���������� ��������� ��������� - �� 4 �� 40000 ��. ���������� �������� �������� ������������ ������������� ������������ (105 ��) ���������������� ���������. ��������, ������������� � �������, ��������� ������������ ��������� ��� ���������� ����������.', 11, '25293199-5ca9-48c6-9168-9f91be142f05'),
('f7d0332b-ac00-44cb-b3b2-2e42ebc30b74', '��������� LED Samsung QE98QN100BUXRU', 3599000.00, 'Resources\Img\Product\��������� LED Samsung QE98QN100BUXRU.jpg', '������������������� ���������� Quantum Matrix ����� ��������� ������������ ������������ Quantum Mini LED. ��������� ������������� ���������� ����������, ������� ������ ������������ ���������� ������������ ��� � ����� ������, ��� � � ����� ����� ������.', 1, 'eb50f925-3a67-4293-8f1b-60fe2732b465'),
('d9ec1695-10ba-48bd-8f9b-30015c90a8e8', 'DEXP micro USB - USB 2.0', 199.00, 'Resources\Img\Product\DEXP micro USB - USB 2.0.jpg', '������ DEXP micro USB - USB ������ ���� ���������� � ����������� ����������� ������ � ������������. ���� ��������� ����� ��� ����������� �������, ���������� �������� microUSB, � USB-������, �������� ����������� ��������� � ������������ ����������, ������������� � ������� �������� ����������, � ����� ������ ������� � ������������ �������. ������ ������������ ��� �������� ������ � ��� ������� ������������� ����������� ���������. ��������� ������������ �������� ���� �� 2.1 � ������������. ���� �������� ����������� ������ ������� ������������� ������� ����� �������. ���������� ������������ ������ ������������ ������� ������������� �����������.', 344, '8d63c1db-d221-4a18-ba7c-305908d93a8b'),
('5cd0cd70-d4ab-49a3-8a6e-34f81a3ac3a8', '����������� Blackmagic Design URSA Broadcast G2 ', 789999.00, 'Resources\Img\Product\����������� Blackmagic Design URSA Broadcast G2.jpg', '����������� Blackmagic Design URSA Broadcast G2 Camera ������������ � ������� ��� ��������� ������ ������� � ��������� �������� 4". ������ � ������� Blackmagic RAW ��������� ������� �������� 6K ��� ����������� �������� � ������� ��������� ������. ���������� ������������ ������ � SD- � ����������� ������� ������ CFast 2.0.', 1, 'ee043a6f-29a9-4a83-963c-dbcdd52a911b'),
('9a8085bd-14aa-42c1-a191-3acfc7f25a9a', 'PlayStation 5 Slim', 79999.00, 'Resources\Img\Product\PlayStation 5 Digital Edition.jpg', '��������� ������� ������� PS5 ������ �������� ������ ������� ����������, ����������� � ���������� � ���������� �������. ���� ������� ���� ������ ������ � ����, ���� �� ������� ������, ��������� ����������� �������������� ���������� �������� 1 ��. �������������� ���� ������� ������ ��������� ����������� ���������� �������� ������������� ��� ��� PS5. ��������� ������ ���������� � ���������� ������������ ���� ��������� ����������� ���������� ���������� �������� �����, � �������� ��������� ���� PS5 �������� �������� �����, ���� � ���������.', 1, 'b87bc505-208b-4789-8062-e12c7ce29e0c'),
('26157dbc-dcdb-4991-838a-4154b2487ccc', '��������� ��������� Hoco L10', 99.00, 'Resources\Img\Product\��������� ��������� Hoco L10 �����.jpg', '��������� ��������� Hoco L10 ���������� ���������� ������ ����������� ���������, ��� ������������ ������� ������� � ��� � ������������ ������������� ������ � ������� ������ ����� ��� ������� ������ �� ����������. ����������� ���������, ����������������� ���������� � ���������� 10 ��, ������������ ������ � �������� �������� � ��������� ��������������� ������ 20-20000 ��. ������� ��������� � ������ ������ �� ����� �� ������� �������� ������ ������������ ���������, � ���������� ���� ��� ����� �������������� ��� ��� ������������� ������, ��� � ��� ���������� ����������.', 54, '8d63c1db-d221-4a18-ba7c-305908d93a8b'),
('4cebeffb-d6c7-42a5-b89d-5742523b97de', '������� Echips Envy', 31000.00, 'Resources\Img\Product\������� Echips Envy.jpg', '��������� ������������ �������, ������������� ���������� ��������� � �������������� ������ �� ������������ �������� �� ����� ������ ���������� ������� ������� Echips Envy. � 4-������� ����������� Intel Celeron J4125 � ����������� ����� Intel UHD Graphics 600 �� ������ ������ �� ��� ������� ������, ���� ��� ������������ ����������� ������� �������� � �� �������� �����������, ���� �� ������������� ��������� ����� ��������� ����.', 12, '4637328f-ce92-48fc-8138-5cbc93b0fe40'),
('91c63ec9-6a8d-4a9d-9370-7baf92f924a0', '�������� EPOS Sennheiser HD 800 S', 150999.00, 'Resources\Img\Product\�������� EPOS Sennheiser HD 800 S.jpg', '��������� �������� Sennheiser HD 800 S �������� ������� ���������, �������������� �������� �������. ���������� ������������ � ������������ ������ ������� � ������������ ������������. ���������� ������������ ������������ ������� ������ �������, �� ���������� ����������� ���� ����� ��������� ����� �������������.', 3, '25293199-5ca9-48c6-9168-9f91be142f05'),
('94cae74e-2abe-41f7-b07b-7dc507844fd7', '��������� ��������� Perfeo ALPHA', 99.00, 'Resources\Img\Product\��������� ��������� Perfeo ALPHA.jpg', '������� �������� ���� ������ ������� � ������� �� ������� ���������, ��������� ���� �� ����� ������������ �� ������ ��� ������������� ������, �� � ��� ���������� �� ��������. ������������ ��������� ������������ ����� ������ ����������� �������, ����� ������������ ���� ��������� ��������� ������ ��� ���������� ������� � ���� � �������� �������������. �� ��������������� ������� ����� � ��������� �������� ������ � ��������� 20-20000 �� � ���� ������ �������� ����������� ������������ ���������� � 10-�������������� ����������.', 34, '8d63c1db-d221-4a18-ba7c-305908d93a8b'),
('ac31b2fc-dc73-4f38-9342-7ef62820a489', 'Samsung Galaxy S21 FE', 45499.00, 'Resources\Img\Product\Samsung Galaxy S21 FE.jpg', '�������� Samsung Galaxy S21 FE �������� � ������ ������� � ������������ ���������� � ���������� ������� �������������� ����������� ������������ ������. � ��� ���������� ����������� ������� 6.4 ����� �� ������ ������ Dynamic AMOLED 2X (2340x1080 ��������), �� ������� ������������ ���������������� � ��������� ��������. ���������� ��������� �� ����� � ����������� Exynos 2100 ����������� �������������� �������.', 20, '9eb9d73e-45fb-47a6-8fff-e1c42c7c552d'),
('3424a8ce-76db-4153-81a5-82157948755a', 'Microsoft Xbox Series S', 34999.00, 'Resources\Img\Product\Microsoft Xbox Series S.jpg', '������� ������� Microsoft Xbox Series S ���������� ����������� ��������� � ��������� ����������� � ������������� ���� � ��������������� ����������� ��������� ��� ������� 120 ������ � �������. ������� ������������������ ����������� ��������� ���������� AMD Zen 2 � 10 �� ����������� ������, � ��� �������� ����� ��������� ��� ������������ ���������� SSD ������� 512 ��. ���������������� ����������� Microsoft Xbox Series S ������������ ����������� Wi-Fi, ����� ������� USB 3.1, ������������ HDMI. � ������ � ������� �������� ������������ ��������� ������������ ����������.', 33, 'b87bc505-208b-4789-8062-e12c7ce29e0c'),
('787e6696-b118-48c0-bdb6-9f401219046f', '��������� LED Econ EX-24HT008B', 6300.00, 'Resources\Img\Product\��������� LED Econ EX-24HT008B.png', '��������� LED Econ EX-24HT008B �������� ����� ������������ ��������� ��� ������������� ��������� �������. �� ���������� ������� 24 ����� � ����������� ������������ HD, ������� �������� ���������� ������������ �������� � ������� ���������. ������� ���������� ����� � ���������� �������� ������� ������� �������� �������� ������������� ����������� � ������ ����������.', 4, 'eb50f925-3a67-4293-8f1b-60fe2732b465'),
('c5af8bc5-235c-4e94-a886-a2a4d1954709', 'Xiaomi Redmi A2', 6499.00, 'Resources\Img\Product\Xiaomi Redmi A2.jpg', '�������� Xiaomi Redmi A2+ � ������� ����� ������� ������������ ��������� ���� SIM-����, ����� �� ����� ������������ ������ � ������� ������. ����� �������� ���������� 6.52" � ����������� 1600x720 ��� ����������� ��������� ������ ��������. ���������� ������ ������������ ������ ������� �� ������ �����������: ����������� � �������. 8-������� ��������� MediaTek Helio G36 ������ � 3 �� ����������� ������ ������������ ����������� ������� ������������������ ��� ������� ��������� ���������� � ������ � ������ ���������������.', 36, '9eb9d73e-45fb-47a6-8fff-e1c42c7c552d'),
('b5e63011-5e93-4b04-b799-ad240f15c49a', 'Apple iPhone 15 Pro Max', 229699.00, 'Resources\Img\Product\Apple iPhone 15 Pro.jpg', '���������� ������� Apple iPhone 15 Pro Max ���� 3-������������ ������ A17 Pro � 6-������� GPU � ���������� ����������� �����. ��� ������ ���������� ������������������ � �����. ����� ������� 16-������� Neural Engine, ������� ������������ 35 ��������� �������� � �������.', 1, '9eb9d73e-45fb-47a6-8fff-e1c42c7c552d'),
('e0f03327-acee-4cdb-bfa8-b0468f8d90d5', '������� DEXP Aquilon', 20000.00, 'Resources\Img\Product\������� DEXP Aquilon.jpg', '������� DEXP Aquilon ���������� ��������� NumPad, ������� �������� ���� �������� ����������. ������������� ��������� ����� ����������� ������ � �� ������������� �� ������������� �������������� ����������. ������� �������� ������ ����� ������������ 2-������� ��������� Intel Celeron N4020. ���������� Intel UHD Graphics 600 ������� �������� ������� ��� ��������� ���������� � ����������.', 45, '4637328f-ce92-48fc-8138-5cbc93b0fe40'),
('22f37c2f-6c2e-4d56-98fe-b269b431e4b5', 'PlayStation 4 Slim', 39999.00, 'Resources\Img\Product\PlayStation 4 Slim.jpg', '������� ������� PlayStation 4 Slim ������������ � �������� ������ ������� � ���������� � ���������� FullHD. ������ �� � ��� 8 �� GDDR5 ����������� ������� ��������, � HDD-���� �� 500 �� - ������� ���������� ������� ���. ����������� � �� ����� HDMI 1.4 ������������ ���������� ������ �����/����� ��� �������� � ����������.', 6, 'b87bc505-208b-4789-8062-e12c7ce29e0c'),
('b7ccaece-3c13-426f-8129-b42ef3f82e06', '����������� DEXP VC A01', 2000.00, 'Resources\Img\Product\����������� DEXP VC A01.jpg', '����������� DEXP VC A01 - ���������� ������ ��� ������ ����, �� ����� ��� �� ���������. ������ ������������ ������������ ������ ������� �������� ����������� FullHD. ������� ������ ����� � ����, � ��� ������������ ������������ ������� ��� �������. CMOS-������� �� 24 �� ��������� ����� ���� ���������� ������. ������������ ����������� �������� ��� ������ � ��������.', 4, 'ee043a6f-29a9-4a83-963c-dbcdd52a911b'),
('5f51f9ad-d6e8-4e9d-8336-c0270a74886c', '���-������ Defender C-090 ', 800.00, 'Resources\Img\Product\���-������ Defender C-090.jpg', '���-������ Defender C-090 - ������ � ������� ��������� ���������. ��������� ��� ���-������ - CMOS. ������� � ����������� ����� � 0.3 ��. ��������� ������ ���������� �������. � ���-������ ������������� ��������� ����- � ������������ ������ � ����������� 640x480. ������������ �������� ������� - 30 ����/�. ������ ��������� ������� ���������� ����������, ����������� ��������� ������� ����������.', 3, 'ee043a6f-29a9-4a83-963c-dbcdd52a911b'),
('74d9df0d-805f-45c2-a0ea-f75a65f96b98', '������� DEXP Atlas', 45000.00, 'Resources\Img\Product\DEXP Atlas.jpg', '������� DEXP Atlas ���������� ���������� �������� � ����� 1.6 �� � ������� �������������� ���������������, ��� ��������� ����������� � �������� �����. ���������� ������������ ������������ ���������� ������������ ����������� Intel Core i5-1235U, 16 �� ����������� ������ � ����������� SSD �������� 512 ��. ������� ������������ � �� Windows 11 Home, ������� ����� � ������ ����� ������� ���������.', 7, '4637328f-ce92-48fc-8138-5cbc93b0fe40'),
('dc53f11c-2b1e-4b05-b978-fc80442ad7a6', 'Apple AirPods Max', 70000.00, 'Resources\Img\Product\Apple AirPods Max.jpg', 'Bluetooth ��������� Apple AirPods Max ��������� ����� ������������ ��������� ������� ���� ������������� � ���������. � ������ ������ ������������ ��������� Apple H1 � ��������� ������������ ��������, ����� ���������� ������� ����� �������� ������� ������� ���������. ��������� �����, � ����� ��������� ������� ����������������� ����� � ������� ������������� ������������ �������� ������ �������� ������� ��� ����� � ������������ ����������� ��� ������������� ����������� ����������, ������ �� ���������� � ����������.', 9, '25293199-5ca9-48c6-9168-9f91be142f05');	

INSERT INTO [order] (id, client_id, order_date, total_amount)
VALUES ('5a14de25-da4b-4bac-8aa8-16b8f336cd63', '7728f6bb-9f31-4648-9093-a3e0c657b7c0', '2023-11-30 03:09:55.367', 688995.00),
('310a4807-7fa2-4b5e-bd8e-214f6929714f', 'f81feb5f-d319-4c77-8db5-ba1acffa935d', '2023-11-30 03:11:32.317', 1194.00),
('3133a60d-d891-4856-b065-41798e19c3cf', '7728f6bb-9f31-4648-9093-a3e0c657b7c0', '2023-11-30 03:10:22.157', 1069698.00),
('82299a73-504a-45e6-a426-4b8873eb6218', '6d625af7-bc56-4345-899d-ba8c67a59171', '2023-11-30 03:13:08.347', 1579998.00),
('be3a6472-6f47-415a-9d1b-5c9c587b85a6', '69d933ab-45e6-4c1e-87f3-b8dbc5da391c', '2023-11-30 03:11:14.390', 140995.00),
('df673f61-50fb-488c-921c-7142064373be', 'f81feb5f-d319-4c77-8db5-ba1acffa935d', '2023-11-30 03:11:46.770', 172996.00),
('8afb2f24-ae75-481c-9b78-cda37d839662', '6d625af7-bc56-4345-899d-ba8c67a59171', '2023-11-30 03:13:26.697', 74000.00),
('c6183e0f-afbc-4372-a331-d6254db73ca9', '7728f6bb-9f31-4648-9093-a3e0c657b7c0', '2023-11-30 03:10:42.700', 79999.00),
('ad2639bd-e95f-4f3c-bea1-f0554ba717f9', '69d933ab-45e6-4c1e-87f3-b8dbc5da391c', '2023-11-30 03:10:55.090', 3755499.00),
('ff443138-1ab7-44b2-8631-f19ebf812332', '57406836-11f8-4c17-9fe9-ba6908226fb7', '2023-11-30 03:12:21.120', 4000.00),
('182a31a4-5df7-4a6e-8839-fa5d8e25cd5f', '57406836-11f8-4c17-9fe9-ba6908226fb7', '2023-11-30 03:12:40.403', 3774975.00);

INSERT INTO order_item (id, order_id, product_id, quantity, amount)
VALUES('1d2cb609-6a9c-4667-a7ad-0185d2d35548', '5a14de25-da4b-4bac-8aa8-16b8f336cd63', '3424a8ce-76db-4153-81a5-82157948755a', 1, 34999.00),
('ee8219d1-ac25-41c0-9fa7-04d77fbc773b', 'be3a6472-6f47-415a-9d1b-5c9c587b85a6', 'dc53f11c-2b1e-4b05-b978-fc80442ad7a6', 2, 140000.00),
('31f625b5-ddb6-48b4-8449-0ac97fbf2d27', 'c6183e0f-afbc-4372-a331-d6254db73ca9', '9a8085bd-14aa-42c1-a191-3acfc7f25a9a', 1, 79999.00),
('289e059a-d0c1-45ad-9343-0de7b5a69a03', 'ff443138-1ab7-44b2-8631-f19ebf812332', 'ac31b2fc-dc73-4f38-9342-7ef62820a489', 1, 45499.00),
('2b97f6a8-a272-49d2-adaa-2ccfd21818f7', '182a31a4-5df7-4a6e-8839-fa5d8e25cd5f', '91c63ec9-6a8d-4a9d-9370-7baf92f924a0', 25, 3774975.00),
('60dd1e2d-bdc1-43e1-b01f-3313be2143ba', '3133a60d-d891-4856-b065-41798e19c3cf', '5cd0cd70-d4ab-49a3-8a6e-34f81a3ac3a8', 1, 789999.00),
('03ca9f44-8f38-461a-bd22-37a6f73f17db', '82299a73-504a-45e6-a426-4b8873eb6218', 'b5e63011-5e93-4b04-b799-ad240f15c49a', 2, 1579998.00),
('eb0486c8-7d9e-4291-abc9-4ea3b2310179', 'df673f61-50fb-488c-921c-7142064373be', 'c5af8bc5-235c-4e94-a886-a2a4d1954709', 2, 12998.00),
('5d1b5cfb-2c70-4e01-b7ce-56493cd730f7', '3133a60d-d891-4856-b065-41798e19c3cf', 'b5e63011-5e93-4b04-b799-ad240f15c49a', 1, 229699.00),
('eff6c0cd-2876-4d0c-9193-62736bbb85b4', 'ad2639bd-e95f-4f3c-bea1-f0554ba717f9', '99790ed4-3467-45f9-a16b-263b1f6d4348', 3, 111000.00),
('2152d68d-09bb-4aa6-b032-66ea8005cf48', 'be3a6472-6f47-415a-9d1b-5c9c587b85a6', 'd9ec1695-10ba-48bd-8f9b-30015c90a8e8', 5, 995.00),
('d4d83e34-3bdb-4924-aa44-67b272d47c7f', 'ff443138-1ab7-44b2-8631-f19ebf812332', 'b7ccaece-3c13-426f-8129-b42ef3f82e06', 2, 4000.00),
('8c62df6d-48c3-481c-a426-87daf614b98e', '5a14de25-da4b-4bac-8aa8-16b8f336cd63', '91c63ec9-6a8d-4a9d-9370-7baf92f924a0', 4, 603996.00),
('2bf73452-94ca-45d8-9f32-883e6dcd6421', 'ad2639bd-e95f-4f3c-bea1-f0554ba717f9', 'f7d0332b-ac00-44cb-b3b2-2e42ebc30b74', 1, 3599000.00),
('783f5997-06b1-46fa-92fc-8f5a72c2fb90', 'df673f61-50fb-488c-921c-7142064373be', '9a8085bd-14aa-42c1-a191-3acfc7f25a9a', 2, 159998.00),
('3ff50e92-1d7c-4e61-af22-936301c359a1', 'ad2639bd-e95f-4f3c-bea1-f0554ba717f9', 'ac31b2fc-dc73-4f38-9342-7ef62820a489', 1, 45499.00),
('0e67bbc6-aef5-4e8c-a117-bf930e90e6e9', '3133a60d-d891-4856-b065-41798e19c3cf', '12533992-11da-4579-95b1-0c32dec8e741', 2, 50000.00),
('c69f518c-99ec-48ef-9324-e38102ee6cd8', '310a4807-7fa2-4b5e-bd8e-214f6929714f', 'd9ec1695-10ba-48bd-8f9b-30015c90a8e8', 6, 1194.00),
('30edbc4e-e953-42a6-b455-ecd299ee4eea', '5a14de25-da4b-4bac-8aa8-16b8f336cd63', '12533992-11da-4579-95b1-0c32dec8e741', 2, 50000.00),
('9f67355e-fb17-4ea3-bfa2-ffdddffefaa0', '8afb2f24-ae75-481c-9b78-cda37d839662', '99790ed4-3467-45f9-a16b-263b1f6d4348', 2, 74000.00);
