export type PagedResults <T> = {
    result: T[];
    pageCount: number;
    totalCount: number;
}
export type Licitatii = {
    pretRezervare: number;
    vanzator: string;
    castigator?: any;
    celMaiMareBid: number;
    createdAt: string;
    lastUpdatedAt: string;
    licitatieEnd: string;
    make: string;
    modelMasina: string;
    an: number;
    culoare: string;
    kilometraj: number;
    imagineUrl: string;
    id: string;
  }
export type Bid = {
    status: any; 
    id: string,
    licitatieId: string,
    vanzator: string,
    bidTime: string,
    pretRezervare: number,
    
}
  
  