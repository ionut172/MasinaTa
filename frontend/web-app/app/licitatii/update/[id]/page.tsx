import Header from "@/app/components/Header";
import React from "react";
import LicitatiiForm from "../../LicitatiiForm";
import { getDetailedViewData } from "@/app/actions/licitatiiActions";

export default async function Update({params}: {params: {id: string}}) {
    const data =  await getDetailedViewData(params.id);
    return (
        <div className="mx-auto max-w-[75%] shadow-lg p-10 bg-white rounded-lg">
            <Header title="Editeaza licitatie" subtitle="Te rugam sa modifici detaliile masinii"/>
            <LicitatiiForm licitatii={data} />
        </div>
    )
}