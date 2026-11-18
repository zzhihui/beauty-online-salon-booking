(function () {
    'use strict';
    angular.module("appointmentModal", [])
    .constant("APPOINTMENT_PERIOD", 30)
    .constant("BUSSINESS_START", 8)
    .constant("BUSSINESS_END", 22)
    .controller("appointmentModalCtrl", function ($log, $scope, $uibModalInstance, bookingItem, bookedTime, bookButtonText, APPOINTMENT_PERIOD, BUSSINESS_START, BUSSINESS_END) {
        $scope.bookingItem = bookingItem;
        $scope.bookButtonText = bookButtonText;
        // 弹出框是否弹出
        $scope.datePickerOpened = false;
        // 按下弹出按钮
        $scope.pickDate = function () {
            $scope.datePickerOpened = true;
        };
        // 设置最大预约日, 最小预约日等
        var today = new Date();
        today.setHours(today.getHours() + 1);
        today.setMinutes(0);
        today.setSeconds(0);
        today.setMilliseconds(0);
        var maxAppointmentDate = new Date();
        maxAppointmentDate.setDate(today.getDate() + APPOINTMENT_PERIOD);
        maxAppointmentDate.setHours(BUSSINESS_END);
        maxAppointmentDate.setMinutes(0);
        $scope.dateOptions = {
            maxDate: maxAppointmentDate,
            minDate: today,
            startingDay: 1,
            maxMode: 'day',
            showWeeks: false
        };
        // 设置最大最小预约时间
        $scope.minBooking = today;
        $scope.maxBooking = maxAppointmentDate;
        // 监视预约时间变化
        $scope.bookingTimeChanged = function () {
            // 当时间格式不正确时该方法会报错, 先验证是否为Date可以避免这个错误
            if (angular.isDate($scope.bookingTime)) {
                var time = $scope.bookingTime;
                var minute = time.getMinutes();
                var hour = time.getHours();
                // 仅能选取15的倍数分钟
                minute = Number.parseInt(minute / 15) * 15;
                time.setMinutes(minute);
                // 无法选择非营业时间
                if (hour > BUSSINESS_END) {
                    time.setHours(BUSSINESS_END);
                }
                if (hour < BUSSINESS_START) {
                    time.setHours(BUSSINESS_START);
                }
                $scope.bookingTime = new Date(time);
            }
        }

        // 预约
        $scope.bookit = function () {
            $uibModalInstance.close($scope.bookingTime);
        };
        // 取消选择
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        }
        // 初始化预约日为今天或已预约时间
        if (angular.isDate(bookedTime)) {
            $scope.bookingTime = bookedTime;
        } else {
            $scope.bookingTime = today;
        }
    });
})();
    