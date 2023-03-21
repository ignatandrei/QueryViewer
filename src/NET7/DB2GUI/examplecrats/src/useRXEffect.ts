import { Observable } from 'rxjs';
import { useEffect,  useState } from 'react';

export default function useRxObs<T>(factory: Observable<T>) {
  const[isLoading, setIsLoading] = useState(true);
    const[error, setError] = useState<any|null>(null);
    const[data, setData] = useState<T|null>(null);
    //var m=useMemo(()=>factory,[factory]);
    useEffect(()=>{      
      var data= factory        
        .subscribe({
        next: (val:T)=>{
          setError('');
          setIsLoading(false);
          // if(funcAfterData)
          //   val=funcAfterData(val);
          setData(val);
      },
      error:(err: any)=>{ 
        setIsLoading(false);
        setError(err); 
      }
    }
      );    
      return ()=> data.unsubscribe();
    },[]);
    return [isLoading, error, data];
}

