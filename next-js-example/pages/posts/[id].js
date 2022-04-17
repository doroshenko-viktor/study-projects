import Head from "next/head";
import Layout from "../../components/layout";
import { getAllPostIds, getPostData } from '../../lib/posts';
import Date from '../../components/date';
import utilStyles from '../../styles/utils.module.css';

export default function Post({ postData }) {
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

export async function getStaticPaths() {
    return {
        paths: getAllPostIds(),
        fallback: false,
    };
};

export async function getStaticProps({ params: { id: postId } }) {
    return {
        props: {
            postData: await getPostData(postId),
        },
    };
};