app.controller('jobscheduleController', ['$scope', 'dataService', '$window', function ($scope, db, window) {

    $scope.isCollapsed = false;

    $scope.save = function (record) {
        console.log(record)
        db.contractor.createSchedule(record).success(function (result) {
            console.log('Created');
        });
    }

    db.contractor.getSchedule().success(function (result) {
        $scope.schedule = result;
    });

    $scope.save = function (record) {
        db.contractor.createSchedule(record).success(function (result) {
            db.contractor.getSchedule().success(function (result) {
                $scope.schedule = result;
            });
        });
    }

    $scope.edit = function (record) {
        console.log(record)
        db.contractor.editSchedule(record).success(function (result) {
            console.log('Updated');
        });
    }

    db.contractor.getSchedule().success(function (result) {
        $scope.schedule = result;
    });


    $scope.delete = function (record) {
        console.log(record)
        db.contractor.deleteSchedule(record).success(function (result) {
            console.log('Deleted');
        });
    }

    db.contractor.getSchedule().success(function (result) {
        $scope.schedule = result;
    });

    $scope.delete = function (record) {
        db.contractor.deleteSchedule(record).success(function (result) {
            db.contractor.getSchedule().success(function (result) {
                $scope.schedule = result;
            });
        });
    }

   
    //Date picker start
    $scope.today = function () {
        $scope.dt = new Date();
    };
    $scope.today();

    $scope.clear = function () {
        $scope.dt = null;
    };

    // Disable weekend selection
    $scope.disabled = function (date, mode) {
        return (mode === 'day' && (date.getDay() === 0 || date.getDay() === 6));
    };

    $scope.open = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();

        $scope.opened = true;
    };

    $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
    $scope.format = $scope.formats[0];


    $scope.getDayClass = function (date, mode) {
        if (mode === 'day') {
            var dayToCheck = new Date(date).setHours(0, 0, 0, 0);

            for (var i = 0; i < $scope.events.length; i++) {
                var currentDay = new Date($scope.events[i].date).setHours(0, 0, 0, 0);

                if (dayToCheck === currentDay) {
                    return $scope.events[i].status;
                }
            }
        }

        return '';
        //Date picker End
    };
}]);