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
}]);
