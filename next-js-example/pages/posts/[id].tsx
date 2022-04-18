import Head from "next/head";
import Layout from "../../components/layout";
import { getAllPostIds, getPostData } from '../../lib/posts';
import Date from '../../components/date';
import utilStyles from '../../styles/utils.module.css';
import { PostData } from "../../types/post";
import { GetStaticPaths, GetStaticProps } from "next";

type Props = {
    postData: PostData,
};

export default function Post({ postData }: Props) {
    return <Layout>
        <Head>
            <title>{postData.title}</title>
        </Head>
        <h1 className={utilStyles.headingXl}>
            {postData.title}
        </h1>
        <div className={utilStyles.lightText}>
            <Date dateString={postData.date} />
        </div>
        <br />
        <article dangerouslySetInnerHTML={{ __html: postData.content }}>
        </article>
    </Layout >;
};

export const getStaticPaths: GetStaticPaths = async () => {
    return {
        paths: getAllPostIds(),
        fallback: false,
    };
};

type GetStaticPropsParams = {
    params: { id: string }
};

export const getStaticProps: GetStaticProps = async ({
    params: { id: postId }
}: GetStaticPropsParams) => {
    return {
        props: {
            postData: await getPostData(postId),
        },
    };
};