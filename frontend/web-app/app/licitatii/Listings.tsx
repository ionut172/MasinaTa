'use client'
import React, { useEffect, useState } from "react"
import LicitatiiCard from "./LicitatiiCard";
import { Licitatii, PagedResults } from "@/types";
import AppPagination from "../components/AppPagination";
import { GetData } from "../actions/licitatiiActions";
import Filters from "../actions/Filters";
import { useParamsStore } from "@/hooks/useParamsStore";
import { shallow } from "zustand/shallow";
import qs from 'query-string';
import Empty from "../components/Empty";


export default function Listings() {
//    const [licitatii, setLicitatii] = useState<Licitatii[]>([]);
//    const [pageCount, setPageCount] = useState(0);
//    const [pageNumber, setPageNumber] = useState(1);
// const [pageSize, setPageSize] = useState(4);
const [data, setData] = useState<PagedResults<Licitatii>>();
const params = useParamsStore(state=>({
    pageNumber: state.pageNumber,
    pageSize: state.pageSize,
    searchTerm: state.searchTerm,
    orderBy: state.orderBy,
    filterBy: state.filterBy,
    vanzator: state.vanzator,
    castigator: state.castigator
}), shallow);
const setParams = useParamsStore(state=>state.setParams);
const url = qs.stringifyUrl({url:'', query: params});
function setPageNumber (pageNumber:number) {
    setParams({pageNumber: pageNumber});
}
   useEffect(()=> {
    GetData(url).then(data=>{
        setData(data);
        
    })
   }, [url])

   if(!data) return <h3>Loading...</h3>

   if(data.totalCount ===0) return <Empty showReset/>

    return (
        <>
        <Filters/>
        <div className="grid grid-cols-4 gap-6">
        {data.result.map(lictatie=> (
            <LicitatiiCard licitatie={lictatie} key={lictatie.id}/>
           
        ))}
        </div>
        <div className="flex float-center space-x-4 justify-center mt-4">
            <AppPagination pageChanged={setPageNumber} currentPage={params.pageNumber} pageCount={data.pageCount}/>
        </div>
        </>
        
     
    );
}