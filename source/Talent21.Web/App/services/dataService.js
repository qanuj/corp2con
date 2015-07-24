app.factory('dataService', ['$http', '$q', function ($http, $q) {
    var v = 'api/v1/';
    var factory = {
        pageSize: 50,
        contractor: {},
        job: {},
        company: {},
        system: {}
    };

    function calculatePaging(page, pageSize) {
        pageSize = pageSize || factory.pageSize;
        var pg = "&$top=" + pageSize;
        if (page > 1) {
            pg += "&$skip=" + ((page - 1) * pageSize);
        }
        return pg;
    }

    factory.role = document.querySelector('html').dataset.role;

    //For Contractors

    factory.contractor.paged = function () {
        return $http.get(v + 'contractor/paged');
    }

    factory.contractor.dashboard = function () {
        return $http.get(v + 'contractor/dashboard');
    }

    factory.contractor.all = function () {
        return $http.get(v + 'contractor/all');
    }

    factory.contractor.get = function (id) {
        return $http.get(v + 'contractor/profile' + (!!id ? '/' + id : ''));
    }

    factory.contractor.update = function (formData) {
        return $http.put(v + 'contractor/profile', formData);
    }

    factory.contractor.search = function (query) {
        return $http.post(v + 'job/search', query);
    }

    factory.contractor.jobById = function (id) {
        return $http.get(v + 'contractor/job/' + id);
    }

    factory.contractor.ApplyToJob = function (id) {
        return $http.post(v + 'contractor/job/' + id + '/apply');
    }

    factory.contractor.applications = function (id) {
        return $http.put(v + '/job/applications/' + id + '/applications');
    }

    factory.contractor.favorite = function (id) {
        return $http.put(v + 'candidate/job/application/' + id + '/favorite');
    }

    factory.contractor.createSchedule = function (formData) {
        return $http.post(v + 'contractor/schedule', formData);
    }

    factory.contractor.editSchedule = function (formData) {
        return $http.put(v + 'contractor/schedule', formData);
    }
    factory.contractor.deleteSchedule = function (record) {
        return $http.delete(v + 'contractor/schedule/' + record.id);
    }

    factory.contractor.getSchedule = function (page) {
        return $http.get(v + 'contractor/schedule/all?$orderby=Id desc' + calculatePaging(page));
    }


    //For companies

    factory.company.paged = function () {
        return $http.get(v + 'company/paged?$inlinecount=allpages');
    }

    factory.company.dashboard = function () {
        return $http.get(v + 'company/dashboard');
    }


    factory.company.all = function () {
        return $http.get(v + 'company/all');
    }

    factory.company.get = function (id) {
        return $http.get(v + 'company/profile' + (!!id ? '/' + id : ''));
    }

    factory.company.searchContractor = function (query) {
        return $http.post(v + 'company/search', query);
    }

    factory.company.update = function (formData) {
        return $http.put(v + 'company/profile', formData);
    }

    factory.company.myJobs = function () {
        return $http.get(v + 'company/job/all');
    }

    factory.company.limitJobs = function () {
        return $http.get(v + 'company/job/paged');
    }

    factory.company.search = function (query) {
        return $http.post(v + 'company/search', query);
    }
    //For Jobs

    factory.job.get = function (id) {
        return $http.get(v + 'company/job/' + id);
    }

    factory.job.paged = function (page) {
        return $http.get(v + 'company/job/paged?$orderby=Id desc' + calculatePaging(page));
    }

    factory.job.create = function (formData) {
        return $http.post(v + 'company/job', formData);
    }

    factory.job.update = function (record) {
        return $http.put(v + 'company/job', record);
    }

    factory.job.publish = function (id) { return $http.put(v + 'company/job/publish', { id: id }); }
    factory.job.cancel = function (id) { return $http.put(v + 'company/job/cancel', { id: id }); }
    factory.job.delete = function (id) { return $http.delete(v + 'company/job/'+id); }

    factory.system.getSkills = function (q) {
        var uri = v + 'system/skill/all';
        if (q) uri += '?$filter=startswith(Title,\'' + q + '\')';
        return $http.get(uri);
    }

    factory.system.addSkill = function (record) {
        return $http.post(v + 'system/skill/create', record);
    }

    factory.system.editSkill = function (formData) {
        return $http.put(v + 'system/skill/update', formData);
    }
    factory.system.getIndustries = function (page) {
        return $http.get(v + 'system/industry/all?$orderby=Id desc' + calculatePaging(page));
    }

    factory.system.addIndustry = function (record) {
        return $http.post(v + 'system/industry/create', record);
    }
    factory.system.updateIndustry = function (record) {
        return $http.put(v + 'system/industry/update', record);
    }
    factory.system.deleteIndustry = function (record) {
        return $http.delete(v + 'system/industry/'+record.id);
    }


    factory.system.getFunctionals = function (page) {
        return $http.get(v + 'system/functional/all?$orderby=Id desc' + calculatePaging(page));
    }

    factory.system.addFunctional = function (record) {
        return $http.post(v + 'system/functional/create', record);
    }
    factory.system.updateFunctional = function (record) {
        return $http.put(v + 'system/functional/update', record);
    }
    factory.system.deleteFunctional = function (record) {
        return $http.delete(v + 'system/functional/'+record.id);
    }

    factory.system.searchLocations=function(address) {
        var params = { address: address, sensor: false };
        return common.$http.get(
          '//maps.googleapis.com/maps/api/geocode/json',
          { params: params }
        );
    }

    return factory;
}]);