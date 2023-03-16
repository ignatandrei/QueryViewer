import { delay, Observable } from 'rxjs';
import { useEffect, useState } from 'react';
import { error } from 'console';
import DatabaseAdmin from './Admin/DatabaseAdmin';

export default function useRxObs<T>(factory: () => Observable<T>) {
  const[isLoading, setIsLoading] = useState(true);
    const[error, setError] = useState<any|null>(null);
    const[data, setData] = useState<T|null>(null);
    useEffect(()=>{      
      var data= factory()
        .pipe(
          delay(1000)
        )
        .subscribe({
        next: (val:T)=>{
          setError('');
          setIsLoading(false);
          setData(val);
      },
      error:(err: any)=>{ 
        setIsLoading(false);
        setError(err); 
      }
    }
      );    
      return ()=> data.unsubscribe();
    },[setData, setIsLoading, setError, factory]);
    return [isLoading, error, data];
}

