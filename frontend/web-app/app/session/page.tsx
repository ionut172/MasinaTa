
import { getSession } from "next-auth/react";
import React, { use } from "react";
import Header from "../components/Header";
import { getCurrentUser, getTokenWorkAround } from "../actions/authActions";
import { UpdateLicitatiiTest } from "../actions/licitatiiActions";
import LicitatiiTest from "./LicitatiiTest";


export default async function session () {
    const session = await getSession();
    const user = await getCurrentUser();
    const token = await getTokenWorkAround();
    console.log(session);
    return (
        <div>
          <Header title="Detalii sesiune" subtitle="Informatii"/>
          <div className="bg-blue-200 border-2 border-blue-500">
            <h3 className="text-lg">
                Date Sesiune
            </h3>
            <pre>
                {JSON.stringify(user,  null, 2)}
            </pre>
          </div>
          <div className="mt-4">
            <LicitatiiTest />
            <div className="bg-green-200 border-2 border-green-500 mt-4">
            <h3 className="text-lg">
                Token data
            </h3>
            <pre className="overflow-auto">
                {JSON.stringify(token,  null, 2)}
            </pre>
        </div>
          </div>
        </div>
    )
}