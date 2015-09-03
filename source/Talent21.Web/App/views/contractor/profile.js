app.controller('contractorProfileController', ['$scope', 'dataService', '$stateParams', function ($scope, db, param) {

    function loadSchedule(page) {
        return db.contractor.getSchedule(page).success(function (result) {
            angular.forEach(result, function (d) {
                d.start = moment(d.start).toDate();
                d.end = moment(d.end).toDate();
            });
            $scope.schedule = result;
        });
    }

    db.system.enums('proficiencyEnum').then(function (enums) {
        $scope.proficiencies = enums;
        db.system.enums('levelEnum').then(function (levels) {
            $scope.levels = levels;
        });
        loadSchedule();
        db.contractor.get(param.id).success(function (result) {
            $scope.record = result;
        });
    });

}]);