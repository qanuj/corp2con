app.controller('companyContractorController', ['$scope', 'dataService', '$stateParams', '$rootScope', function ($scope, db, $stateParams, $rootScope) {
    
    $scope.shortlist=function(id) {
        
    }

    function loadSchedule(id) {
        return db.company.getSchedule(id).success(function (result) {
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
        loadSchedule(param.id);

        db.contractor.get(param.id).success(function (result) {
            $scope.record = result;
            db.company.visitContractor(param.id);
        });
    });

    $scope.move = function (id, folder) {
        db.company.moveContractor(id, folder).success(function () {
            console.log('moved');
        });
    }

    $scope.download = function (id, folder) {
        db.company.moveContractor(id, folder).success(function () {
            console.log('moved');
        });
    }

}]);