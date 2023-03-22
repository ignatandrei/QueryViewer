export enum SearchCriteria
{
    None = 0,
    StartsWith,
    EndsWith,
    Contains,
    Equal,
    InArray,
    NotInArray,
    Between,
    NotBetween,
    Different,
    Greater,
    Less,
    GreaterOrEqual,
    LessOrEqual,
    //Like,

    EqualYear,
    DifferentYear,
    GreaterYear,
    LessYear,
    GreaterOrEqualYear,
    LessOrEqualYear,

    //EqualMonthYear,
    //DifferentMonthYear,
    //GreaterMonthYear,
    //LessMonthYear,
    //GreaterOrEqualMonthYear,
    //LessOrEqualMonthYear,

    //EqualDay,
    //DifferentDay,
    //GreaterDay,
    //LessDay,
    //GreaterOrEqualDay,
    //LessOrEqualDay,

}

export const SearchCriteriaArray = Object.values(SearchCriteria).filter((v) => isNaN(Number(v)));
export default class columnTable{
    
    public constructor(cc :Partial<columnTable> ){
        if (cc != null) 
        Object.assign(this,cc);
    }
    public name : string='';
    public type: string='';
    public isnullable: boolean=true;
    public ispk:boolean=false;
    public typejs:string='';
    public SupportedOperators(): SearchCriteria[]{
        var ret : SearchCriteria[]=[];
        ret.push(SearchCriteria.Equal);
        ret.push(SearchCriteria.Different);
        switch(this.typejs){
            case 'string':
                ret.push(SearchCriteria.StartsWith);
                ret.push(SearchCriteria.EndsWith);
                ret.push(SearchCriteria.Contains);
                break;
            case 'number':
                ret.push(SearchCriteria.Greater);
                ret.push(SearchCriteria.Less);
                ret.push(SearchCriteria.GreaterOrEqual);
                ret.push(SearchCriteria.LessOrEqual);
                break;
            default:
                console.error('Not supported type in search criteria ' + this.typejs);
                break;
        }
        return ret;
        
    }


}