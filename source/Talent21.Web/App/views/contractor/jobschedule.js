app.controller('jobscheduleController', ['$scope', 'dataService', '$window', '$timeout',function ($scope, db, window, $timeout) {

    $scope.isCollapsed = false;

    $scope.save = function (record) {
        console.log(record)
        db.contractor.createSchedule(record).success(function (result) {
            console.log('Created');
        });
    }

    db.contractor.getSchedule().success(function (result) {
        $scope.schedule = result;
        console.log(result);
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

    /* For Datepicker */

    $scope.today = function () {
        $scope.dt = new Date();
    };
    $scope.today();

        formatYear: 'yy',
    $scope.dateOptions = {
        startingDay: 1
    };

    $scope.open = function () {

        $timeout(function () {
            $scope.opened = true;

    $scope.ok = function () {
        $modalInstance.close($scope.dt);
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
        });
    };

    };

    /* Datepicker Ends here*/
}]);
