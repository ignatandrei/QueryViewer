import { useMatches } from "react-router-dom";
import { SearchData } from "./DatabaseTableSelector";

const useSearchURL = () => {
  var showAll = false;
  let matches = useMatches();
  // console.log("useSearchURL", matches);
  var s: SearchData | null = null;
  if (matches && matches.length > 0) {
    var parms = matches.map((it) => it.params).filter((it) => it != null);
    //console.log("a", parms);
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
          };
        }
      );
    }
  }
 // console.log("c", showAll);
  return [showAll, s];
};

export default useSearchURL;
