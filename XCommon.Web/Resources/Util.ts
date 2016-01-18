class Util {
    constructor() {

    }

    static GetClassName(inputClass: any): string {
        var funcNameRegex = /function (.{1,})\(/;
        var results = (funcNameRegex).exec((<any>inputClass).toString());
        return (results && results.length > 1) ? results[1] : "";
    }

    static Injector = {
        $http: "$http",
        $scope: "$scope",
        $location: "$location"
    };
}

module MyPetLife.Entity {
    export enum EntityAction {
        New = 1
    }
}