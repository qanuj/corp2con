app.controller('companyContractorController', ['$scope', 'dataService', '$stateParams', '$rootScope', function ($scope, db, param, $rootScope) {
        $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = true;
    $rootScope.settings.layout.pageSidebarClosed = false;

    $scope.shortlist=function(id) {
        
    }

    $scope.folders = [
        "Shortlisted",
        "Rejected"
    ];

    db.system.enums('proficiencyEnum').then(function (enums) {
        $scope.proficiencies = enums;
        db.system.enums('levelEnum').then(function (levels) {
            $scope.levels = levels;
        });
        db.company.getContractor(param.id).success(function (result) {
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