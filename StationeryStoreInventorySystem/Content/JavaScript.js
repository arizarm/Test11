//DatePicker

       $(function() {
           $("#txtSDate").datepicker({
               changeMonth: true,
               changeYear: true,
               yearRange: "-0:+2", // You can set the year range as per as your need
               dateFormat: 'dd-M-yy'

           }).val()
           $("#txtEDate").datepicker({
               changeMonth: true,
               changeYear: true,
               yearRange: "-0:+2", // You can set the year range as per as your need
               dateFormat: 'dd-M-yy'
           }).val()
       });
       function printDiv() {
           var divName = "printable";
           var printContents = document.getElementById(divName).innerHTML;
           var originalContents = document.body.innerHTML;

           document.body.innerHTML = printContents;

           window.print();

           document.body.innerHTML = originalContents;
       }


