"use strict";
const PHONE_INPUT_MASK = '+375 (99) 999-99-99';

$('document').ready(function () {
    //$.ajaxSetup({ cache: false });

    //$('#datetimepicker').datetimepicker(
    //    {
    //        format: 'DD/MM/YYYY',
    //        locale: 'en-GB',
    //        minDate: moment(),
    //        //defaultDate : moment(),
    //        maxDate: moment().add(7, 'days')
    //    });
    //$('#timepicker').datetimepicker(
    //    {
    //        format: 'HH:mm',
    //        locale: 'en-GB',
    //        //defaultDate: moment(),
    //        minDate: moment({ h: 9 }),
    //        maxDate: moment({ h: 22 })
    //        //datepicker: false
    //    });

    /////////////Phone Mask
    $("#phone").inputmask(PHONE_INPUT_MASK);

    $('.spoiler-explore').click(function () {
        $(this).parent().children('div.spoiler-content').toggle('fast');
        return false;
    });
});