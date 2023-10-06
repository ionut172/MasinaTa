import Header from "@/app/components/Header";
import Head from "next/head";
import React from "react";
import LicitatiiForm from "../LicitatiiForm";

export default function Create() {
    return (
        <div className="mx-auto max-w-[75%] shadow-lg p-10 bg-white rounded-lg">
            <Header title="Vinde-ti masina!" subtitle="Introdu acum detaliile masinii tale"/>
            <LicitatiiForm/>
        </div>
    )
}