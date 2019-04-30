# Adresler ve işlemler

varsayılan adres ```http://globalmedia.local/```

### add
yeni kayıt adresi
    ```http://globalmedia.local/add/[serialnumber]```
- content-type: 'text/plain'
- outputs = true|false

### wakeup
bilgisayarın açıldığını ve beklemede olduğunu bildirir
    ```http://globalmedia.local/wakeup/[serialnumber]```
- content-type: 'text/plain'
- outputs = true|false

### state
bilgisayarın ekran kilidinin açılıp açılmayacağını sorgular
    ```http://globalmedia.local/state/[serialnumber]```
- content-type: 'text/plain'
- outputs = true|false

### shutdown
bilgisayarın kapatıldığını bildirir
    ```http://globalmedia.local/shutdown/[serialnumber]```
- content-type: 'text/plain'
- outputs = null