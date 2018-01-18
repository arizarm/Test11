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


