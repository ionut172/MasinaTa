import { getDetailedViewData } from "@/app/actions/licitatiiActions";
import Header from "@/app/components/Header";
import React from "react";
import CountdownTimer from "../../CountdownTimer";
import LicitatiiImage from "../../LicitatiiImage";
import DetailedSpecs from "./DetailedSpecs";
import { getCurrentUser } from "@/app/actions/authActions";
import EditButton from "./EditButton";
import DeleteButton from "./DeleteButton";

export default async function Details({params}: {params: {id: string} }) {
    const data = await getDetailedViewData(params.id);
    const user = await getCurrentUser();
    return (
        <div>
            <div className="flex justify-between">
            <Header title={`${data.make} ${data.modelMasina}`} subtitle="Detalii despre masina"/>
            {user?.username === data.vanzator && (
                <>
                <EditButton id={data.id}></EditButton>
                <DeleteButton id={data.id}></DeleteButton>
                </>
            )}
            <div className="flex gap-3">
                <h3 className="text-2xl font-semibold" >
                        Timp ramas
                </h3>
                <CountdownTimer licitatieEnd={data.licitatieEnd} />
            </div>
            </div>
          <div className="grid grid-cols-2 gap-6 mt-3">
                <div className="w-full bg-gray-200 aspect-h-10 aspect-w-16 rounded-lg overflow-hidden">
                    <LicitatiiImage imagineUrl={data.imagineUrl}/>
                </div>
        
          <div className="border-2 rounded-lg p-2 bg-gray-100">
                <Header title="Licitatii" subtitle="Pleaseaza licitatia"/>
          </div>
          <div className="mt-3 grid grid-cols-1 rounded-lg">
               <DetailedSpecs licitatii={data}/>
          </div>   
          </div> 
        </div>
    )
}