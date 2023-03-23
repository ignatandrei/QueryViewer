import { useMatches } from "react-router-dom";
import { SearchCriteria, SearchCriteriaArray } from "./column";
import { SearchData } from "./DatabaseTableSelector";

const useSearchURL = () => {
  let matches = useMatches();
  // console.log("useSearchURL", matches);
    var s: SearchData = new SearchData();
    var showAll = false;
  if (matches && matches.length > 0) {
    var parms = matches.map((it) => it.params).filter((it) => it != null);
    console.log("a", parms);
    if (parms.length > 0) {
      parms.forEach((parm) => {
        var keys = Object.keys(parm);
        //console.log("b", keys);
        if (keys.length > 0) {        
            if (keys.some((it) => it === "what")) {
              if (parm["what"] === "showall") {                
                  showAll = true;
              }
            }
            if (keys.some((it) => it === "columnName")) {
                s.ColumnName= parm["columnName"]||'';
            }
            if (keys.some((it) => it === "operator")) {
              var val=(parm["operator"]||'').toLowerCase();
              s.Operator= SearchCriteriaArray.findIndex(it=>it.toString().toLowerCase()===val);
              //console.log('this',val, s.Operator);
            }
            if (keys.some((it) => it === "value")) {
                s.Value = parm["value"]||'';
            }
          };
        }
      );
    }
  }
  
  return [showAll,s];
};

export default useSearchURL;
